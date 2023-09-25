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
        Normal,
        Move,
        Delete,
        Teleport,
        Camera,
        Hopin,
        Rec
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
    // UI GameObject array
    public GameObject[] modeUIImage;

    void Start()
    { 
        UI.Player_State = UI.PlayerState.Normal;
    }

    void Update()
    {
        //모드 별 UI
        if(UI.Player_State == UI.PlayerState.Normal)
        {
            //초기화 값
            ModeUI(5);
        }

        else if(UI.Player_State == UI.PlayerState.Delete)
        {
            ModeUI(0);
        }

        else if (UI.Player_State == UI.PlayerState.Move)
        {
            ModeUI(3);
        }

        else if (UI.Player_State == UI.PlayerState.Teleport)
        {
            ModeUI(2);
        }

        else if (UI.Player_State == UI.PlayerState.Hopin)
        {
            ModeUI(1);
        }

        else if (UI.Player_State == UI.PlayerState.Camera)
        {
            ModeUI(4);
        }

        //클릭하면 모드 전환하게 해야함
        /*if (OVRInput.GetDown(button, controller))
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
        }*/
    }

    // -----------------------------------소원이 부분---------------------------------------// 

    // 기본 셋팅 값
    public void OnNormal()
    {
        print("기본 모드");
        UI.Player_State = UI.PlayerState.Normal;
    }

    // 플레이어 Delete 모드
    public void OnDelete()
    {
        print("삭제 모드 활성화");
        UI.Player_State = UI.PlayerState.Delete;
    }

    // 플레이어 Move 모드
    public void OnMove()
    {
        print("이동 모드 활성화");
        UI.Player_State = UI.PlayerState.Move;
    }

    // Player Teleport 모드
    public void OnTeleport()
    {
        print("텔레포트 모드 활성화");
        UI.Player_State = UI.PlayerState.Teleport;
    }

    // -----------------------------------현숙이 부분---------------------------------------// 

    // Player Hopin 모드
    public void OnHopIn()
    {
        print("들어가는 모드 활성화");
        UI.Player_State = UI.PlayerState.Hopin;
    }


    // Player Camera 모드
    public void OnCamera()
    {
        print("카메라 모드 활성화");
        UI.Player_State = UI.PlayerState.Camera;
    }

    // Input number = true
    // other number = false
    public void ModeUI(int num)
    {
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