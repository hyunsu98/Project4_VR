using System;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
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
    public GameObject cube; // 플레이어의 부모가 될 게임 오브젝트

    // 플레이어를 Teleport할 때 필요한 요소
    public GameObject player;
    public Transform marker; // marker
    // 마커 크기 조절하는 공식 변수(?)
    public float kAdjust = 0.1f;

    //가지고 있는 플레이어
    GameObject inPlayer;
    //따라다니게 하는 코드
    bool isPlayerPut;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        marker.localScale = Vector3.one * kAdjust;
    }

    void Update()
    {
        // 손위치에서 손의 앞방향으로 Ray를 만들고
        Ray ray = new Ray(hand.position, hand.forward);
        lr.SetPosition(0, hand.position);

        bool isHit = Physics.Raycast(ray, out hitInfo);

        //닿은 곳이 있다면
        if (isHit)
        {
            lr.SetPosition(1, hitInfo.point);
            // 큐브의 위치가 레이에 닿은 위치이다.

            //큐브를 계속 따라다니게 하고 싶다.
            cube.transform.position = hitInfo.point;

            marker.position = hitInfo.point;
            marker.up = hitInfo.normal;
            marker.localScale = Vector3.one * kAdjust * hitInfo.distance;

            // 플레이어 배치 모드 일때
            /*if (UI.Player_State == UI.PlayerState.Player)
            {
                if (isPlayerPut)
                {
                    inPlayer.transform.position = hitInfo.point;
                    inPlayer.transform.SetParent(cube.transform);
                }
            }*/

            if (isPlayerPut)
            {
                inPlayer.transform.position = hitInfo.point;
                inPlayer.transform.SetParent(cube.transform);
            }

            //현숙추가 -> UI 창에서는 플레이어가 보이지 않게 하기 위해
            if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("UI"))
            {
                if(inPlayer != null)
                {
                    inPlayer.SetActive(false);
                }
                #region 버튼 스크립트 (보류)
                // 버튼 스크립트를 가져온다
                /*Button btn = hitInfo.transform.GetComponent<Button>();
                // 만약 btn이 null이 아니라면
                if (btn != null)
                {
                    print("버튼 클릭");
                    btn.onClick.Invoke();
                }*/
                #endregion
            }

            else
            {
                if (inPlayer != null)
                {
                    inPlayer.SetActive(true);
                }
            }

            // 부딪힌 곳이 있다면 Two 버튼
            if (OVRInput.GetDown(button, controller))
            { 
                // 플레이어 배치 모드
                /*if (UI.Player_State == UI.PlayerState.Player)
                {
                    Debug.Log("Player 배치 모드");

                    if (isPlayerPut)
                    {
                        //땅일 때만 놓을 수 있게
                        if(hitInfo.collider.CompareTag("Ground"))
                        {
                            inPlayer.transform.SetParent(null);
                            inPlayer.GetComponent<Collider>().enabled = true;

                            //초기화 셋팅
                            isPlayerPut = false;
                            inPlayer = null;
                        }
                    }
                }*/


                Debug.Log("Player 배치 모드");

                if (isPlayerPut)
                {
                    //땅일 때만 놓을 수 있게
                    if (hitInfo.collider.CompareTag("Ground"))
                    {
                        inPlayer.transform.SetParent(null);
                        inPlayer.GetComponent<Collider>().enabled = true;

                        //초기화 셋팅
                        isPlayerPut = false;
                        inPlayer = null;
                    }
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
            print("되고있니?");
            Debug.Log(hitInfo.collider.name);
            //  Ray 닿는 곳으로 이동하고 싶다.
            player.transform.position = hitInfo.point;
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

    //플레이어 클릭 
    public void Player(string name)
    {
        // 플레이어 모드일때만 클릭하면 생겨야 함  -> UI모드로 바꿔야 할 거 같음
        /*if(UI.Player_State == UI.PlayerState.Player)
        {
            if(inPlayer == null)
            {
                // 플레이어 활성화 모드!
                print("캐릭터 생김");
                GameObject tmp = Resources.Load(name) as GameObject;
                GameObject obj = Instantiate(tmp);

                // 플레이어 셋팅
                inPlayer = obj;
                isPlayerPut = true;
            }
        }*/

        if (inPlayer == null)
        {
            // 플레이어 활성화 모드!
            print("캐릭터 생김");
            GameObject tmp = Resources.Load(name) as GameObject;
            GameObject obj = Instantiate(tmp);

            // 플레이어 셋팅
            inPlayer = obj;
            isPlayerPut = true;
        }
    }
}
