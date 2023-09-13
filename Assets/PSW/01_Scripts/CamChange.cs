using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamChange : MonoBehaviour
{
    public Transform player1;
    public Transform player2;

    private Transform currentTarget; // ���� ī�޶� ���

    void Start()
    {
        // �ʱ� ī�޶� ����� player1���� ����
        currentTarget = player1;

        // ī�޶� Player1�� �ڽ� ������Ʈ�� ����ϴ�.
        transform.SetParent(player1);
        //transform.localPosition =  // ī�޶��� ���� ��ġ�� ����(0, 0, 0)���� ����
    }

    void Update()
    {
        // G Ű�� ���� �� ī�޶� ����� ��ȯ
        if (Input.GetKeyDown(KeyCode.G))
        {
            // ���� ī�޶� ����� player1�̸� player2��, �� �ݴ��� ��쵵 ó��
            currentTarget = (currentTarget == player1) ? player2 : player1;

            // ī�޶��� �θ� �����Ͽ� ���� ����� �ڽ����� ����ϴ�.
            transform.SetParent(currentTarget);
            //transform.localPosition = Vector3.zero; // ���� ��ġ�� �������� ����
        }
    }
}
