using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //��Ʈ�ѷ��� Thumbstick�� �и� ������ �̵�, ���� �����̵�.

    void Start()
    {
       
    }

    void Update()
    {
        ZoomProcess();
    }

    private void ZoomProcess()
    {
        // ��Ʈ�ѷ��� Thumbstick�� �и� ���, ���� Ȯ���ϰ�ʹ�.(CameraScope�� FOV�� �ǵ帮�ڴ�)

        Vector2 axis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);

        print(axis.y);
    }
}
