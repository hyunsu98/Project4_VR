using System;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class Player_Ray : MonoBehaviour
{
    public OVRInput.Button button;
    public OVRInput.Controller controller;

    //오른손에서 그려지는 ray
    public Transform hand;
    LineRenderer lr;

    RaycastHit hitInfo;

    // 플레이어를 배치 하기 위해 필요한 요소
    bool isPlacingPlayer = false; // 플레이어 놓은건지 아닌지 묻기
    GameObject spawnObject; // 땅 위치 설정하기 
    Vector3 placementPosition; // Ray로 놓아질 위치
    bool isClickPending = false; // 클릭 대기 상태를 추적
    public float maxLineDistance = 3f; // Ray에 최대 길이

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // 손위치에서 손의 앞방향으로 Ray를 만들고
        Ray ray = new Ray(hand.position, hand.forward);
        lr.SetPosition(0, hand.position);


        if (Physics.Raycast(ray, out hitInfo))
        {
            lr.SetPosition(1, hitInfo.point);

            // 부딪힌 곳이 있다면
            if (OVRInput.GetDown(button, controller))
            {
                // 플레이어 1, 2 배치 모드
                if (UI.Player_State == UI.PlayerState.Player)
                {
                    Debug.Log("Player1 배치 모드");
                    //Player(name);
                }

                // 플레이어 Move 모드
                if (UI.Player_State == UI.PlayerState.Move)
                {
                    Debug.Log("Player Move 모드");
                    Move();
                }

                // 플레이어 Delete 모드
                if (UI.Player_State == UI.PlayerState.Delete)
                {
                    Debug.Log("Player Delete 모드");
                    Delete();
                }

                // Player Teleport 모드
                if (UI.Player_State == UI.PlayerState.Teleport)
                {
                    Debug.Log("Player Teleport 모드");
                    TelePort();
                }

                // Player Camera 모드
                if (UI.Player_State == UI.PlayerState.Camera)
                {
                    Debug.Log("Player Camera 모드");
                    Cam();
                }

                // Player Hopin 모드
                if (UI.Player_State == UI.PlayerState.Hopin)
                {
                    Debug.Log("Player Hopin 모드");
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
        //모드 별로 하면 될 것 같음
        //만약 닿은곳이 Enemy라면
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
            print("캐릭터 생김");
            GameObject tmp = Resources.Load(name) as GameObject;

            GameObject obj = Instantiate(tmp);
            obj.transform.position = hitInfo.point;


            /* print("캐릭터1");

             if (!isPlacingPlayer)
             {
                 print("캐릭터2");
                 GameObject tmp = Resources.Load(name) as GameObject;

                 isPlacingPlayer = true;

                 placementPosition = hand.position + hand.forward * maxLineDistance;

                 Vector3 spawnPosition = hitInfo.point;
                 //Debug.Log(hitInfo.collider.name);
                 //hitInfo.collider.gameObject;

                 // 플레이어의 위치를 땅의 표면으로 조정
                 spawnPosition.y = GetGroundHeight(spawnPosition);
                 GameObject obj = Instantiate(tmp, spawnPosition, Quaternion.identity);
                 spawnObject = obj;

                 // 첫번째 클릭이 발생한 후 두번째 클릭을 할 수 있게 설정해주는 것
                 isClickPending = true;
             }
             else
             {
                 placementPosition = hand.position + hand.forward * maxLineDistance;

                 if (spawnObject != null)
                 {
                     // 플레이어의 위치를 땅의 표면으로 조정해주는 코드
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

    // 특정 위치에서 땅의 높이를 검색하는 역할
    // void Player(string name) 함수에 쓰일 함수 작성
    private float GetGroundHeight(Vector3 position)
    {
        Ray ray = new Ray(position + Vector3.up * 100f, Vector3.down);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            return hitInfo.point.y;
        }

        return 0f; // 땅을 찾지 못한 경우 기본값으로 0을 반환
    }

    // 두번째 이벤트를 실행해주기 위해 필요한 함수
    // void Player(string name) 함수에 쓰일 함수 작성
    private void PlacePlayer()
    {
        if (spawnObject != null)
        {
            isClickPending = false; // 두 번째 클릭 이벤트가 처리됨
        }
        lr.enabled = false;
        isPlacingPlayer = false;
    }

    void HopIn()
    {
        //모드 별로 하면 될 것 같음
        //만약 닿은곳이 Enemy라면
        if (hitInfo.collider.CompareTag("Player"))
        {
            Debug.Log(hitInfo.collider.name);
            PlayerMove.instance.CharChange(hitInfo.collider.gameObject);
        }
    }
}
