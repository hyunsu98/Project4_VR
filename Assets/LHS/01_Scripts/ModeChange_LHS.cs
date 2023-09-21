using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Switch;

public static class UI
{
    public static PlayerState playerState;

    // UI ��ư�� ���� ���� ��ȯ
    public enum PlayerState
    {
        Player1,
        Player2,
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

    //��ȯ������ϴ� ��.
    public OVRInputModule enentSystem;
    public OVRGazePointer ovrGazePointer;

    public Transform centerEye;
    public Transform rightHand;

    public bool isCenter;

    public GameObject modeUI;

    public bool isHopln;

    void Start()
    {

    }

    void Update()
    {
        //Ŭ���ϸ� ��� ��ȯ�ϰ� �ؾ���
        if (OVRInput.GetDown(button, controller))
        {
            print("��ư ��� ��ȯ");
            isCenter = !isCenter;

            if (isCenter)
            {
                enentSystem.rayTransform = centerEye;
                ovrGazePointer.rayTransform = centerEye;

                //UI�� ������?
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

    //��� �ٲ�� �ؾ�������
    public void OnHopIn()
    {
        print("���� ��� Ȱ��ȭ");
        UI.Player_State = UI.PlayerState.Hopin;
    }

    public void OnCam()
    {
        print("ī�޶� ��� Ȱ��ȭ");
    }

    // -----------------------------�ҿ��� �κ�---------------------------------------
    // 
    public void OnMove() // ��ġ�� ��
    {

    }
}
