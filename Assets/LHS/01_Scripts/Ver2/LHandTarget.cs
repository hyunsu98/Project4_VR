using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�÷��̾ ����ٴ� Ÿ��
public class LHandTarget : MonoBehaviour
{
    public Transform target;
    public Transform idle;

    public bool isTargeting;

    void Start()
    {
        //�����Ҷ� �� ����
        if(idle != null)
        {
            transform.position = idle.transform.position;
            transform.rotation = idle.transform.rotation;
        }
    }

    void Update()
    {
        //��ȭ���϶�
        if (isTargeting)
        {
            //����Ǿ�� �� ������
            transform.position = target.transform.position;
            transform.rotation = target.transform.rotation;
        }

        //��ȭ�� �ƴҶ�
        else
        {

        }
    }
}


