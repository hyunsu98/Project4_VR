using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ModeChange_LHS : MonoBehaviour
{
    public OVRInput.Button button;
    public OVRInput.Controller controller;

    //전환해줘야하는 값.
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
        //클릭하면 모드 전환하게 해야함
        if(OVRInput.GetDown(button, controller))
        {
            print("버튼 모드 전환");
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

    //모드 바뀌게 해야할지도
    public void OnHopIn()
    {
        print("들어가는 모드 활성화");
    }

    public void OnCam()
    {
        print("카메라 모드 활성화");
    }

}
