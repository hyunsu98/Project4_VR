using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Switch;

public static class UI
{
    public static PlayerState playerState;

    // UI 버튼에 따른 상태 전환
    public enum PlayerState
    {
        Player,
        Move,
        Delete,
        Teleport,
        Camera,
        Hopin
    }

    public static PlayerState Player_State
    {
        get
        {
            return playerState;
        }

        set
        {
            playerState = value;
        }
    }
}
public class ModeChange_LHS : MonoBehaviour
{
    //public static ModeChange_LHS instance;

    public OVRInput.Button button;
    public OVRInput.Controller controller;

    //전환해줘야하는 값.
    public OVRInputModule enentSystem;
    public OVRGazePointer ovrGazePointer;

    public Transform centerEye;
    public Transform rightHand;

    public bool isCenter;

    public GameObject modeUI;

    public bool isHopln;

    // UI GameObject array
    public GameObject[] modeUIImage;
    void Start()
    {
        print("현재 모드" + UI.Player_State);
    }

    void Update()
    {

        //클릭하면 모드 전환하게 해야함
        if (OVRInput.GetDown(button, controller))
        {
            print("R + hand 클릭");
            isCenter = !isCenter;

            if (isCenter)
            {
                enentSystem.rayTransform = centerEye;
                ovrGazePointer.rayTransform = centerEye;

                //UI도 꺼지면?
                modeUI.SetActive(true);
            }
            else
            {
                enentSystem.rayTransform = rightHand;
                ovrGazePointer.rayTransform = rightHand;

                modeUI.SetActive(false);
            }
        }
    }

    // -----------------------------------소원이 부분---------------------------------------// 


    // 플레이어 1 ,2 배치 모드
    public void OnPlayer(string name)
    {
        print("플레이어1 배치 활성화");
        UI.Player_State = UI.PlayerState.Player;
    }

    // 플레이어 Delete 모드
    public void OnDelete()
    {
        print("삭제 모드 활성화");
        UI.Player_State = UI.PlayerState.Delete;
        ModeUI(0);
    }

    // 플레이어 Move 모드
    public void OnMove()
    {
        print("이동 모드 활성화");
        UI.Player_State = UI.PlayerState.Move;
        ModeUI(3);
    }

    // Player Teleport 모드
    public void OnTeleport()
    {
        print("텔레포트 모드 활성화");
        UI.Player_State = UI.PlayerState.Teleport;
        ModeUI(2);
    }

    // -----------------------------------현숙이 부분---------------------------------------// 
    //모드 바뀌게 해야할지도
    // Player Hopin 모드
    public void OnHopIn()
    {
        print("들어가는 모드 활성화");
        UI.Player_State = UI.PlayerState.Hopin;
        ModeUI(1);
    }


    // Player Camera 모드
    public void OnCamera()
    {
        print("카메라 모드 활성화");
        UI.Player_State = UI.PlayerState.Camera;
        ModeUI(4);
    }

    // Input number = true
    // other number = false
    public void ModeUI(int num)
    {
        print("num 확인");
        for (int i = 0; i < modeUIImage.Length; i++)
        {
            if (i == num)
            {
                modeUIImage[num].SetActive(true);
            }
            else 
            {
                modeUIImage[i].SetActive(false);
            }
        } 
    }


}