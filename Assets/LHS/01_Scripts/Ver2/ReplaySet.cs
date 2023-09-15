using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���÷��� ���
public class ReplaySet : MonoBehaviour
{
    public static ReplaySet instance;

    //ReplayInfo<GameObject> replayInfo;
    //���ӿ�����Ʈ
    public List<GameObject> unit;

    public bool isPlay;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        // saveModel ���� ������ �÷��̾���� ����ؾ���.
    }

    // �÷��̾� ����� �ִ� ����!!

    // ����� �ִ� ���÷��̸� ��� ����! -> ���÷��� Ŭ�������� 
    public void OnRecordPlay()
    {
        if (unit.Count > 0)
        {
            //����� ��ü�� �Լ��� �ݺ��ؼ� �θ���.!
            for (int i = 0; i < unit.Count; i++)
            {
                var playerRecored = unit[i].GetComponent<PlayerRecord>();
                playerRecored.OnRecordPlay();
                isPlay = false;
            }

            print(unit.Count);
        }

        //���� ���ٸ� ����� �÷��̾ ���ٰ� ǥ��
        else
        {
            print("����� �� ����.");
        }
    }

    public void OnAutoReplayForRecording(PlayerRecord who)
    {
        if (unit.Count > 0)
        {
            //����� ��ü�� �Լ��� �ݺ��ؼ� �θ���.!
            for (int i = 0; i < unit.Count; i++)
            {
                var playerRecored = unit[i].GetComponent<PlayerRecord>();
                if (who == playerRecored)
                    continue;
                playerRecored.OnRecordPlay();
                isPlay = false;
            }

            print(unit.Count);
        }

        //���� ���ٸ� ����� �÷��̾ ���ٰ� ǥ��
        else
        {
            print("����� �� ����.");
        }
    }
}
