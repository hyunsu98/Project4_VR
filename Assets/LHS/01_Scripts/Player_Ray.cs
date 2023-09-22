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
    public GameObject cube; // �÷��̾��� �θ� �� ���� ������Ʈ

    // �÷��̾ Teleport�� �� �ʿ��� ���
    public GameObject player;
    public Transform marker; // marker
    // ��Ŀ ũ�� �����ϴ� ���� ����(?)
    public float kAdjust = 0.1f;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        marker.localScale = Vector3.one * kAdjust;
    }

    void Update()
    {
        // ����ġ���� ���� �չ������� Ray�� �����
        Ray ray = new Ray(hand.position, hand.forward);
        lr.SetPosition(0, hand.position);

        bool isHit = Physics.Raycast(ray, out hitInfo);

        if (isHit)
        {
            lr.SetPosition(1, hitInfo.point);
            // ť���� ��ġ�� ���̿� ���� ��ġ�̴�.
            cube.transform.position = hitInfo.point;
            marker.position = hitInfo.point;
            marker.up = hitInfo.normal;
            marker.localScale = Vector3.one * kAdjust * hitInfo.distance;

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
            marker.position = ray.origin + ray.direction * 100;
            marker.up = -ray.direction;
            marker.localScale = Vector3.one * kAdjust * 100;
        }
    }

    private void Cam()
    {
        
    }

    void TelePort()
    {
        if (hitInfo.collider.CompareTag("Ground"))
        {
            print("�ǰ��ִ�?");
            Debug.Log(hitInfo.collider.name);
            //  Ray ��� ������ �̵��ϰ� �ʹ�.
            player.transform.position = hitInfo.point;
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
