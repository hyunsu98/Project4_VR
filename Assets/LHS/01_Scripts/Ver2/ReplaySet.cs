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

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        
    }

    // �÷��̾� ����� �ִ� ����!!

    // ����� �ִ� ���÷��̸� ��� ����! -> ���÷��� Ŭ�������� 
    public void OnRecordPlay()
    {
        if(unit.Count > 0 )
        {
            //����� ��ü�� �Լ��� �ݺ��ؼ� �θ���.!
            for (int i = 0; i < unit.Count; i++)
            {
                unit[i].GetComponent<PlayerRecord>().OnRecordPlay();
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
