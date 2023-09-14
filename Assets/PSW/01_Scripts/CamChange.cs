using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamChange : MonoBehaviour
{
    public Transform player1; // �÷��̾� 1�� Transform ������Ʈ�� �Ҵ��մϴ�.
    public Transform player2; // �÷��̾� 2�� Transform ������Ʈ�� �Ҵ��մϴ�.
    public Transform vrCamera; // VR ī�޶��� Transform ������Ʈ�� �Ҵ��մϴ�.
    // CenterEye
    public Transform trCenterEye;
    private bool isPlayer1Active = true; // ���� Ȱ��ȭ�� �÷��̾ �����մϴ�.

    void Start()
    {
      
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            // G Ű�� ���� ������ �÷��̾ �ٲߴϴ�.
            isPlayer1Active = !isPlayer1Active;

            // ���� Ȱ��ȭ�� �÷��̾��� ��ġ�� VR ī�޶� �̵��մϴ�.
            if (isPlayer1Active)
            {
                vrCamera.position = player1.position;
            }
            else
            {
                vrCamera.position = player2.position;
            }
        }
    }
}
