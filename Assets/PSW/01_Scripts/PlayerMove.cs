using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // �̵� �ӵ�
    public float speed = 5;
    // CharacterController ������Ʈ
    CharacterController cc;
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // ������� �Է¿� ���� �������ķ� �̵��ϰ� �ʹ�.
        // 1. ������� 
    }
}
