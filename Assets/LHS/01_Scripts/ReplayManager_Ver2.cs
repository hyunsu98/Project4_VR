using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//저장되야할 정보 : 오브젝트이름, 시간, 위치, 회전, 팔 위치, 음성파일
[System.Serializable]
public class PlayerInfo
{
    //이름
    GameObject gameObject;
    //시간
    public float time;
    //위치
    public Vector3 pos;
    //회전
    public Quaternion rot;

    //HandAnchor 팔 위치 (왼팔 / 오른팔)
    public LeftHandInfo leftHand;

    public LeftHandInfo rightHand;
}

//왼팔
[System.Serializable]
public class LeftHandInfo
{
    public Vector3 pos;
    public Quaternion rot;
}

//오른팔
[System.Serializable]

public class RightHandInfo
{
    public Vector3 pos;
    public Quaternion rot;
}

//※사운드

//객체의 녹화된 프레임을 담을 클래스
[System.Serializable]
public class PlayerJsonList<T>
{
    public List<T> playerJsonList;
}
/*public class PlayerJsonList<PlayerInfo>
{
    public List<PlayerInfo> list;
}*/

public class ReplayManager_Ver2 : MonoBehaviour
{
    //녹화여부
    bool isRecord;
    bool isReplay;
    //※재생될 객체의 갯수로 하면! -> 변경
    int count = 2;
    //어떤 객체를 실행 시킬것인지
    int who;
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
    List<PlayerJsonList<PlayerInfo>> loadList;

    private void Start()
    {
        // 시작할 때 녹화중 아님
        isRecord = false;

        //List  생성
        //저장 될 List
        saveList = new PlayerJsonList<PlayerInfo>();
        saveList.playerJsonList = new List<PlayerInfo>();

        //불러올 List
        loadList = new List<PlayerJsonList<PlayerInfo>>();
    }

    private void Update()
    {
        //녹화중
        if(isRecord)
        {
            Recording(who);
        }
        //리플레이중
        else if(isReplay)
        {
            Replaying();
        }
    }

    //리플레이
    private void Replaying()
    {
        curTime += Time.deltaTime;

        //재생 될 객체의 수만큼 반복한다.
        for (int i = 0; i < unit.Length; i++)
        {
            Debug.Log(unit.Length);

            //저장된 리스트의 0번째부터
            PlayerInfo info = loadList[i].playerJsonList[loadIndex];
            unit[i].transform.position = info.pos;
            unit[i].transform.rotation = info.rot;

            if(i == unit.Length - 1 && curTime >= info.time)
            {
                loadIndex++;

                if(loadIndex >= loadList[i].playerJsonList.Count)
                {
                    isReplay = false;
                    print("Stop");
                }
            }
        }
    }

    //녹화
    private void Recording(int who)
    {
        curTime += Time.deltaTime;

        totalTime += Time.deltaTime;

        //0.1초 간격으로 저장
        if(curTime > recordTime)
        {
            curTime -= recordTime;

            SavePlayerInfo(who);
        }
    }

    //저장
    private void SavePlayerInfo(int who)
    {
        PlayerInfo info = new PlayerInfo()
        {
            time = totalTime,
            pos = unit[who].transform.position,
            rot = unit[who].transform.rotation,
            leftHand = null,
            rightHand = null
        };

        saveList.playerJsonList.Add(info);
    }

    // 녹화 시작
    public void OnRecordStart(int who)
    {
        this.who = who;
        isRecord = true;
        //처음부터 녹화
        saveList.playerJsonList.Clear();
    }

    public void OnRecordEnd()
    {
        isRecord = false;

        //파일 쓰기
        string json = JsonUtility.ToJson(saveList, true);

        File.WriteAllText(Application.dataPath + "/save" + who + ".txt", json);
    }

    public void OnRecordPlay()
    {

        if(isRecord)
        {
            OnRecordEnd();
        }
        isReplay = true;

        for(int i = 0; i < unit.Length; i++)
        {
            string json = File.ReadAllText(Application.dataPath + "/save" + i + ".txt");

            var jsonList = JsonUtility.FromJson<PlayerJsonList<PlayerInfo>>(json);
            loadList.Add(jsonList);
        }

        loadIndex = 0;
        curTime = 0;

    }
}
