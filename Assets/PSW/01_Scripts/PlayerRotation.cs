using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public Transform vrCamera; // VR 카메라 객체의 Transform
    //public Transform vrCamera1; // VR 카메라 객체의 Transform
    //public Transform vrCamera2; // VR 카메라 객체의 Transform

    private void Start()
    {
    }


    void Update()
    {
        //벡터변수를 만든다.
        Vector3 vector3;
        //그 변수에 나의 위치를 넣는다.
        vector3 = transform.position;
        //만든 변수의 x 값은 vrCamera 의 x 값으로 한다.
        vector3.x = vrCamera.position.x;
        //만든 변수의 z 값은 vrCamera 의 z 값으로 한다.
        vector3.z = vrCamera.position.z;
        //나의 위치를 만든변수로 셋팅한다.
        transform.position = vector3;


        // VR 카메라의 앞 방향을 플레이어 객체의 앞 방향으로 설정
        /*Vector3 cameraForward = vrCamera.forward;
        cameraForward.y = 0;*/ // 플레이어 객체의 회전은 y 축만 고려하므로 y 축을 0으로 설정
        //transform.forward = vrCamera.forward;
        //transform.forward = vrCamera1.forward;
        //transform.forward = vrCamera2.forward;
        //this.transform.LookAt(vrCamera);
        Vector3 v = vrCamera.forward;
        v.y = 0;
        transform.forward = v;
    }
}
