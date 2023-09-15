using System.Collections.Generic;
using System.IO;
using UnityEngine;

//객체마다 가지고 있을 녹화 기능
public class PlayerRecord : MonoBehaviour
{
    //녹화여부
    bool isRecord;
    bool isReplay;

    // 로드 될 객체
    int loadIndex = 0;

    //시간
    float totalTime;
    float curTime = 0;
    float recordTime = 0.1f;

    //저장/재생 될 플레이어들 -> 녹화된 객체가 있으면 추가!
    public GameObject[] unit;

    //녹화된 객체들을 담을 List
    PlayerJsonList<PlayerInfo> saveList;

    //녹화된 객체들을 불러올 List
    PlayerJsonList<PlayerInfo> loadList;

    public List<UnitInfo> unitList = new List<UnitInfo>();

    //팔 오브젝트
    LHandTarget lHand;
    RHandTarget rHand;

    private void Start()
    {
        // 시작할 때 녹화중 아님
        isRecord = false;

        //List  생성
        //저장 될 List
        saveList = new PlayerJsonList<PlayerInfo>();
        saveList.playerJsonList = new List<PlayerInfo>();

        //불러올 List
        loadList = new PlayerJsonList<PlayerInfo>();

        lHand = transform.GetComponentInChildren<LHandTarget>();
        rHand = transform.GetComponentInChildren<RHandTarget>();
    }

    private void Update()
    {
        //녹화중
        if (isRecord)
        {
            Recording();
        }
        //리플레이중
        else if (isReplay)
        {
            Replaying();
        }
    }

    //나의 저장되 정보를 불러오면 됨.
    private void Replaying()
    {
        curTime += Time.deltaTime;

        //저장된 리스트의 0번째부터
        //PlayerInfo info = loadList[i].playerJsonList[loadIndex];
        //PlayerInfo info = unitList[who].loadInfo.playerJsonList[unitList[who].unitLoadIndex];

        //0부터 시작 
        PlayerInfo info = saveList.playerJsonList[loadIndex];

        transform.position = Vector3.Lerp(transform.position, info.pos, 1);
        transform.rotation = Quaternion.Lerp(transform.rotation, info.rot, 1);

        lHand.transform.position = info.leftHand.pos;
        lHand.transform.rotation = info.leftHand.rot;

        rHand.transform.position = info.rightHand.pos;
        rHand.transform.rotation = info.rightHand.rot;

        if (curTime >= info.time) //i == unitList.Count && curTime >= info.time
        {
            print("시간" + info.time);
            //각 객체의 인덱스 길이만큼 추가해야 한다.
            loadIndex++;

            Debug.Log($"객체 이름 {info.name},  읽을인덱스 {loadIndex} , list 카운트 수 {saveList.playerJsonList.Count}");

            if (loadIndex >= saveList.playerJsonList.Count)
            {
                isReplay = false;
                print("Stop" + saveList.playerJsonList.Count);
            }
        }
    }

    //녹화
    private void Recording()
    {
        curTime += Time.deltaTime;

        totalTime += Time.deltaTime;

        //0.1초 간격으로 저장
        if (curTime > recordTime)
        {
            curTime -= recordTime;

            SavePlayerInfo();
        }
    }

    //저장
    private void SavePlayerInfo()
    {
        LeftHandInfo leftHandInfo = new LeftHandInfo()
        {
            pos = lHand.transform.position,
            rot = lHand.transform.rotation
        };

        RightHandInfo rightHandInfo = new RightHandInfo()
        {
            pos = rHand.transform.position,
            rot = rHand.transform.rotation
        };

        PlayerInfo info = new PlayerInfo()
        {
            name = gameObject.name,
            time = totalTime,
            pos = transform.position,
            rot = transform.rotation,

            leftHand = leftHandInfo,
            rightHand = rightHandInfo
        };

        saveList.playerJsonList.Add(info);
    }


    // 녹화 시작
    public void OnRecordStart()
    {
        Debug.Log(gameObject.name + "녹화시작");
        isRecord = true;

        //처음부터 녹화
        saveList.playerJsonList.Clear();

        //내 녹화할 때 재생될 플레이어가 있으면 
        if (ReplaySet.instance.isPlay)
        {
            if (ReplaySet.instance.unit.Count > 0)
            {
                ReplaySet.instance.OnAutoReplayForRecording(this);
                print("녹화될 재생플레이어가 있다");
            }
        }

        else
        {
            print("녹화될 재생플레이어가 없다");
        }
    }


    public void OnRecordEnd()
    {
        Debug.Log(gameObject.name + "녹화종료");

        isRecord = false;

        //파일 쓰기
        string json = JsonUtility.ToJson(saveList, true);
        File.WriteAllText(Application.dataPath + "/save" + gameObject.name + ".txt", json);
    }

    public void OnRecordPlay()
    {
        print("리플레이");

        if (isRecord)
        {
            OnRecordEnd();
        }

        isReplay = true;
        isRecord = false;

        string json = File.ReadAllText(Application.dataPath + "/save" + gameObject.name + ".txt");

        saveList = JsonUtility.FromJson<PlayerJsonList<PlayerInfo>>(json);

        loadIndex = 0;
        curTime = 0;
    }
}
