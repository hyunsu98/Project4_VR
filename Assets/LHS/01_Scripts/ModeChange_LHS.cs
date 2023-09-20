using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ModeChange_LHS : MonoBehaviour
{
    public OVRInput.Button button;
    public OVRInput.Controller controller;

    //��ȯ������ϴ� ��.
    public OVRInputModule enentSystem;
    public OVRGazePointer ovrGazePointer;

    public Transform centerEye;
    public Transform rightHand;

    public bool isCenter;

    public GameObject modeUI;

    void Start()
    {
        
    }

    void Update()
    {
        //Ŭ���ϸ� ��� ��ȯ�ϰ� �ؾ���
        if(OVRInput.GetDown(button, controller))
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
    }

    public void OnCam()
    {
        print("ī�޶� ��� Ȱ��ȭ");
    }

}
