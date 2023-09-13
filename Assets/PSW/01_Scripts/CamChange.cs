using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamChange : MonoBehaviour
{
    public Transform player1;
    public Transform player2;

    private Transform currentTarget; // 현재 카메라 대상

    void Start()
    {
        // 초기 카메라 대상을 player1으로 설정
        currentTarget = player1;

        // 카메라를 Player1의 자식 오브젝트로 만듭니다.
        transform.SetParent(player1);
        //transform.localPosition =  // 카메라의 로컬 위치를 원점(0, 0, 0)으로 설정
    }

    void Update()
    {
        // G 키를 누를 때 카메라 대상을 전환
        if (Input.GetKeyDown(KeyCode.G))
        {
            // 현재 카메라 대상이 player1이면 player2로, 그 반대의 경우도 처리
            currentTarget = (currentTarget == player1) ? player2 : player1;

            // 카메라의 부모를 변경하여 현재 대상의 자식으로 만듭니다.
            transform.SetParent(currentTarget);
            //transform.localPosition = Vector3.zero; // 로컬 위치를 원점으로 설정
        }
    }
}
