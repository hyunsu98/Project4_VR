using System;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class Player_Ray : MonoBehaviour
{
    public OVRInput.Button button;
    public OVRInput.Controller controller;

    //�����տ��� �׷����� ray
    public Transform hand;
    LineRenderer lr;

    RaycastHit hitInfo;

    // �÷��̾ ��ġ �ϱ� ���� �ʿ��� ���
    bool isPlacingPlayer = false; // �÷��̾� �������� �ƴ��� ����
    GameObject spawnObject; // �� ��ġ �����ϱ� 
    Vector3 placementPosition; // Ray�� ������ ��ġ
    bool isClickPending = false; // Ŭ�� ��� ���¸� ����
    public float maxLineDistance = 3f; // Ray�� �ִ� ����

    // �÷��̾ �ڷ���Ʈ �� �� �ʿ��� ���
    public GameObject cube;
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
            // ť���� ��ġ�� ���̿� ���� ��ġ�̴�.
            cube.transform.position = hitInfo.point;


            // �ε��� ���� �ִٸ� Two ��ư
            if (OVRInput.GetDown(button, controller))
            {
                // �÷��̾� 1, 2 ��ġ ���
                if (UI.Player_State == UI.PlayerState.Player)
                {
                    Debug.Log("Player1 ��ġ ���");

                    print(num);
                    num += 1;
                    //Player(name);
                }

                // �÷��̾� Move ���
                if (UI.Player_State == UI.PlayerState.Move)
                {
                    Debug.Log("Player Move ���");
                    Move();
                }

                // �÷��̾� Delete ���
                if (UI.Player_State == UI.PlayerState.Delete)
                {
                    Debug.Log("Player Delete ���");
                    Delete();
                }

                // Player Teleport ���
                if (UI.Player_State == UI.PlayerState.Teleport)
                {
                    Debug.Log("Player Teleport ���");
                    TelePort();
                }

                // Player Camera ���
                if (UI.Player_State == UI.PlayerState.Camera)
                {
                    Debug.Log("Player Camera ���");
                    Cam();
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
            lr.SetPosition(1, ray.origin + ray.direction * 10);
        }
    }

    private void Cam()
    {
        
    }

    void TelePort()
    {
        if (hitInfo.collider.CompareTag("Player"))
        {
            Debug.Log(hitInfo.collider.name);
            cube.transform.position = hitInfo.collider.transform.position;
        }
    }

    void Delete()
    {
        //��� ���� �ϸ� �� �� ����
        //���� �������� Enemy���
        if (hitInfo.collider.CompareTag("Player"))
        {
            Debug.Log(hitInfo.collider.name);
            Destroy(hitInfo.collider.gameObject);
        }
    }

    void Move()
    {
        
    }

    int num = 0;

    //������ �ִ� �÷��̾�
    GameObject inPlayer;

    public void Player(string name)
    {
        if(UI.Player_State == UI.PlayerState.Player)
        {
            print("ĳ���� ����");
            GameObject tmp = Resources.Load(name) as GameObject;
            GameObject obj = Instantiate(tmp);

            inPlayer = obj;

            //print(num);

            obj.transform.position = hitInfo.point;
            obj.transform.SetParent(cube.transform);

            if(num > 1)
            {
                obj.transform.SetParent(null);
                obj.GetComponent<Collider>().enabled = true;
                num = 0;
            }
            //Ŭ�� �� �ѹ� ���� - ������ ĳ���� ����
            //������ ĳ���Ͱ� ������ Ŭ���ص� �� �������� �ʴ´�

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
