using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���÷��� ���
public class ReplaySet : MonoBehaviour
{
    public static ReplaySet instance;

    //���ӿ�����Ʈ
    public GameObject[] unit;

    PlayerRecord playerRecored;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Update()
    {
        //��ġ�� �÷��̾ �� ���
        unit = GameObject.FindGameObjectsWithTag("Player");
    }

    // ����� �ִ� ���÷��̸� ��� ����! -> ���÷��� Ŭ�������� 
    public void OnRecordPlay()
    {
        if (unit.Length > 0)
        {
            //����� ��ü�� �Լ��� �ݺ��ؼ� �θ���.!
            for (int i = 0; i < unit.Length; i++)
            {
                var playerRecored = unit[i].GetComponent<PlayerRecord>();

                playerRecored.OnRecordPlay();
            }

            print(unit.Length);
        }

        //���� ���ٸ� ����� �÷��̾ ���ٰ� ǥ��
        else
        {
            print("����� �� ����.");
        }
    }

    public void OnAutoReplayForRecording(PlayerRecord who)
    {
        if (unit.Length > 0)
        {
            //����� ��ü�� �Լ��� �ݺ��ؼ� �θ���.!
            for (int i = 0; i < unit.Length; i++)
            {
                var playerRecored = unit[i].GetComponent<PlayerRecord>();

                //�� ���� �ݺ�
                if (who == playerRecored)
                { 
                    continue;
                }

                playerRecored.OnRecordPlay();
            }

            print(unit.Length);
        }

        //���� ���ٸ� ����� �÷��̾ ���ٰ� ǥ��
        else
        {
            print("����� �� ����.");
        }
    }
}
