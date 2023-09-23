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
        print("���� ���" + UI.Player_State);
    }

    void Update()
    {

        //Ŭ���ϸ� ��� ��ȯ�ϰ� �ؾ���
        if (OVRInput.GetDown(button, controller))
        {
            print("R + hand Ŭ��");
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

    // -----------------------------------�ҿ��� �κ�---------------------------------------// 

    // �÷��̾� 1 ,2 ��ġ ���
    public void OnPlayer(string name)
    {
        print("�÷��̾�1 ��ġ Ȱ��ȭ");
        UI.Player_State = UI.PlayerState.Player;
    }

    // �÷��̾� Delete ���
    public void OnDelete()
    {
        print("���� ��� Ȱ��ȭ");
        UI.Player_State = UI.PlayerState.Delete;
    }

    // �÷��̾� Move ���
    public void OnMove()
    {
        print("�̵� ��� Ȱ��ȭ");
        UI.Player_State = UI.PlayerState.Move;
    }

    // Player Teleport ���
    public void OnTeleport()
    {
        print("�ڷ���Ʈ ��� Ȱ��ȭ");
        UI.Player_State = UI.PlayerState.Teleport;
    }

    // -----------------------------------������ �κ�---------------------------------------// 
    //��� �ٲ�� �ؾ�������
    // Player Hopin ���
    public void OnHopIn()
    {
        print("���� ��� Ȱ��ȭ");
        UI.Player_State = UI.PlayerState.Hopin;
    }

    // Player Camera ���
    public void OnCamera()
    {
        print("ī�޶� ��� Ȱ��ȭ");
        UI.Player_State = UI.PlayerState.Camera;
    }
}