using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RHandTarget : MonoBehaviour
{
    public Transform target;

    public bool isTargeting;

    void Start()
    {
        
    }

    // Update is called once per frame
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
