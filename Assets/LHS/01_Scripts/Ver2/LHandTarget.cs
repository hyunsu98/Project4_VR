using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�÷��̾ ����ٴ� Ÿ��
public class LHandTarget : MonoBehaviour
{
    public Transform target;

    public bool isTargeting;

    void Start()
    {
        
    }

    void Update()
    {
        //��ȭ���϶�
        if(isTargeting)
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
