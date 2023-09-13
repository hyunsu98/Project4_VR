using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����Ǿ��� ���� : ������Ʈ�̸�, �ð�, ��ġ, ȸ��, �� ��ġ, ��������
[System.Serializable]
public class PlayerInfo
{
    //�̸�
    GameObject gameObject;
    //�ð�
    public float time;
    //��ġ
    public Vector3 pos;
    //ȸ��
    public Quaternion rot;

    //HandAnchor �� ��ġ (���� / ������)
    public LeftHandInfo leftHand;

    public LeftHandInfo rightHand;
}

//����
[System.Serializable]
public class LeftHandInfo
{
    public Vector3 pos;
    public Quaternion rot;
}

//������
[System.Serializable]

public class RightHandInfo
{
    public Vector3 pos;
    public Quaternion rot;
}

//�ػ���

//��ü�� ��ȭ�� �������� ���� Ŭ����
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
    //��ȭ����
    bool isRecord;
    bool isReplay;
    //������� ��ü�� ������ �ϸ�! -> ����
    int count = 2;
    //� ��ü�� ���� ��ų������
    int who;
    // �ε� �� ��ü
    int loadIndex = 0;

    //�ð�
    float totalTime;
    float curTime = 0;
    float recordTime = 0.1f;

    //����/��� �� �÷��̾�� -> ��ȭ�� ��ü�� ������ �߰�!
    public GameObject[] unit;

    //��ȭ�� ��ü���� ���� List
    PlayerJsonList<PlayerInfo> saveList;

    //��ȭ�� ��ü���� �ҷ��� List
    List<PlayerJsonList<PlayerInfo>> loadList;

    private void Start()
    {
        // ������ �� ��ȭ�� �ƴ�
        isRecord = false;

        //List  ����
        //���� �� List
        saveList = new PlayerJsonList<PlayerInfo>();
        saveList.playerJsonList = new List<PlayerInfo>();

        //�ҷ��� List
        loadList = new List<PlayerJsonList<PlayerInfo>>();
    }

    private void Update()
    {
        //��ȭ��
        if(isRecord)
        {
            Recording(who);
        }
        //���÷�����
        else if(isReplay)
        {
            Replaying();
        }
    }

    //���÷���
    private void Replaying()
    {
        curTime += Time.deltaTime;

        //��� �� ��ü�� ����ŭ �ݺ��Ѵ�.
        for (int i = 0; i < unit.Length; i++)
        {
            Debug.Log(unit.Length);

            //����� ����Ʈ�� 0��°����
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

    //��ȭ
    private void Recording(int who)
    {
        curTime += Time.deltaTime;

        totalTime += Time.deltaTime;

        //0.1�� �������� ����
        if(curTime > recordTime)
        {
            curTime -= recordTime;

            SavePlayerInfo(who);
        }
    }

    //����
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

    // ��ȭ ����
    public void OnRecordStart(int who)
    {
        this.who = who;
        isRecord = true;
        //ó������ ��ȭ
        saveList.playerJsonList.Clear();
    }

    public void OnRecordEnd()
    {
        isRecord = false;

        //���� ����
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
