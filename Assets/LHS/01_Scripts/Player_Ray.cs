using UnityEngine;

public class Player_Ray : MonoBehaviour
{
    public OVRInput.Button button;
    public OVRInput.Controller controller;

    //�����տ��� �׷����� ray
    public Transform hand;
    LineRenderer lr;

    RaycastHit hitInfo;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // ����ġ���� ���� �չ������� Ray�� �����
        Ray ray = new Ray(hand.position, hand.forward);
        lr.SetPosition(0, hand.position);


        if (Physics.Raycast(ray, out hitInfo))
        {
            lr.SetPosition(1, hitInfo.point);

            // �ε��� ���� �ִٸ�
            if (OVRInput.GetDown(button, controller))
            {
                // �÷��̾� 1 ��ġ ���
                if (UI.Player_State == UI.PlayerState.Player1)
                {
                    Debug.Log("Player1 ��ġ ���");
                }

                // �÷��̾� 2 ��ġ ���
                if (UI.Player_State == UI.PlayerState.Player2)
                {
                    Debug.Log("Player2 ��ġ ���");
                }

                // �÷��̾� Move ���
                if (UI.Player_State == UI.PlayerState.Move)
                {
                    Debug.Log("Player Move ���");
                }

                // �÷��̾� Delete ���
                if (UI.Player_State == UI.PlayerState.Delete)
                {
                    Debug.Log("Player Delete ���");
                }

                // Player Teleport ���
                if (UI.Player_State == UI.PlayerState.Teleport)
                {
                    Debug.Log("Player Teleport ���");
                }

                // Player Camera ���
                if (UI.Player_State == UI.PlayerState.Camera)
                {
                    Debug.Log("Player Camera ���");
                }

                // Player Hopin ���
                if (UI.Player_State == UI.PlayerState.Hopin)
                {
                    Debug.Log("Player Hopin ���");
                    HopIn();
                }
            }
        }

        else
        {
            lr.SetPosition(1, ray.origin + ray.direction * 1000);
        }
    }

    void HopIn()
    {
        //��� ���� �ϸ� �� �� ����
        //���� �������� Enemy���
        if (hitInfo.collider.CompareTag("Player"))
        {
            Debug.Log(hitInfo.collider.name);
            PlayerMove.instance.CharChange(hitInfo.collider.gameObject);
        }
    }
}
