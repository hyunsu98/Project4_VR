using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public Transform vrCamera; // VR ī�޶� ��ü�� Transform
    //public Transform vrCamera1; // VR ī�޶� ��ü�� Transform
    //public Transform vrCamera2; // VR ī�޶� ��ü�� Transform

    private void Start()
    {
    }


    void Update()
    {
        //���ͺ����� �����.
        Vector3 vector3;
        //�� ������ ���� ��ġ�� �ִ´�.
        vector3 = transform.position;
        //���� ������ x ���� vrCamera �� x ������ �Ѵ�.
        vector3.x = vrCamera.position.x;
        //���� ������ z ���� vrCamera �� z ������ �Ѵ�.
        vector3.z = vrCamera.position.z;
        //���� ��ġ�� ���纯���� �����Ѵ�.
        transform.position = vector3;


        // VR ī�޶��� �� ������ �÷��̾� ��ü�� �� �������� ����
        /*Vector3 cameraForward = vrCamera.forward;
        cameraForward.y = 0;*/ // �÷��̾� ��ü�� ȸ���� y �ุ ����ϹǷ� y ���� 0���� ����
        //transform.forward = vrCamera.forward;
        //transform.forward = vrCamera1.forward;
        //transform.forward = vrCamera2.forward;
        //this.transform.LookAt(vrCamera);
        Vector3 v = vrCamera.forward;
        v.y = 0;
        transform.forward = v;
    }
}
