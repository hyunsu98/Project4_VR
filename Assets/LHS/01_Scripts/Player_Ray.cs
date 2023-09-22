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

    // 플레이어를 텔레포트 할 때 필요한 요소
    public GameObject cube;
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
            // 큐브의 위치가 레이에 닿은 위치이다.
            cube.transform.position = hitInfo.point;


            // 부딪힌 곳이 있다면 Two 버튼
            if (OVRInput.GetDown(button, controller))
            {
                // 플레이어 1, 2 배치 모드
                if (UI.Player_State == UI.PlayerState.Player)
                {
                    Debug.Log("Player1 배치 모드");

                    print(num);
                    num += 1;
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
        if (hitInfo.collider.CompareTag("Player"))
        {
            Debug.Log(hitInfo.collider.name);
            cube.transform.position = hitInfo.collider.transform.position;
        }
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

    int num = 0;

    //가지고 있는 플레이어
    GameObject inPlayer;

    public void Player(string name)
    {
        if(UI.Player_State == UI.PlayerState.Player)
        {
            print("캐릭터 생김");
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
            //클릭 시 한번 생성 - 생성된 캐릭터 있음
            //생성된 캐릭터가 있으면 클릭해도 또 생성되지 않는다

        }  
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
