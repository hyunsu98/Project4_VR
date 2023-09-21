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
                // �÷��̾� 1, 2 ��ġ ���
                if (UI.Player_State == UI.PlayerState.Player)
                {
                    Debug.Log("Player1 ��ġ ���");
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

    public void Player(string name)
    {
        if(UI.Player_State == UI.PlayerState.Player)
        {
            print("ĳ���� ����");
            GameObject tmp = Resources.Load(name) as GameObject;

            GameObject obj = Instantiate(tmp);
            obj.transform.position = hitInfo.point;


            /* print("ĳ����1");

             if (!isPlacingPlayer)
             {
                 print("ĳ����2");
                 GameObject tmp = Resources.Load(name) as GameObject;

                 isPlacingPlayer = true;

                 placementPosition = hand.position + hand.forward * maxLineDistance;

                 Vector3 spawnPosition = hitInfo.point;
                 //Debug.Log(hitInfo.collider.name);
                 //hitInfo.collider.gameObject;

                 // �÷��̾��� ��ġ�� ���� ǥ������ ����
                 spawnPosition.y = GetGroundHeight(spawnPosition);
                 GameObject obj = Instantiate(tmp, spawnPosition, Quaternion.identity);
                 spawnObject = obj;

                 // ù��° Ŭ���� �߻��� �� �ι�° Ŭ���� �� �� �ְ� �������ִ� ��
                 isClickPending = true;
             }
             else
             {
                 placementPosition = hand.position + hand.forward * maxLineDistance;

                 if (spawnObject != null)
                 {
                     // �÷��̾��� ��ġ�� ���� ǥ������ �������ִ� �ڵ�
                     placementPosition.y = GetGroundHeight(placementPosition);
                     spawnObject.transform.position = placementPosition;
                 }

                 if (OVRInput.GetDown(button, controller))
                 {
                     PlacePlayer();
                 }
             }*/
        }  
    }

    // Ư�� ��ġ���� ���� ���̸� �˻��ϴ� ����
    // void Player(string name) �Լ��� ���� �Լ� �ۼ�
    private float GetGroundHeight(Vector3 position)
    {
        Ray ray = new Ray(position + Vector3.up * 100f, Vector3.down);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            return hitInfo.point.y;
        }

        return 0f; // ���� ã�� ���� ��� �⺻������ 0�� ��ȯ
    }

    // �ι�° �̺�Ʈ�� �������ֱ� ���� �ʿ��� �Լ�
    // void Player(string name) �Լ��� ���� �Լ� �ۼ�
    private void PlacePlayer()
    {
        if (spawnObject != null)
        {
            isClickPending = false; // �� ��° Ŭ�� �̺�Ʈ�� ó����
        }
        lr.enabled = false;
        isPlacingPlayer = false;
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
