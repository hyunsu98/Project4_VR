using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamChange : MonoBehaviour
{
    public Transform player1; // 플레이어 1의 Transform 컴포넌트를 할당합니다.
    public Transform player2; // 플레이어 2의 Transform 컴포넌트를 할당합니다.
    public Transform vrCamera; // VR 카메라의 Transform 컴포넌트를 할당합니다.
    // CenterEye
    public Transform trCenterEye;
    private bool isPlayer1Active = true; // 현재 활성화된 플레이어를 추적합니다.

    void Start()
    {
      
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            // G 키를 누를 때마다 플레이어를 바꿉니다.
            isPlayer1Active = !isPlayer1Active;

            // 현재 활성화된 플레이어의 위치로 VR 카메라를 이동합니다.
            if (isPlayer1Active)
            {
                vrCamera.position = player1.position;
            }
            else
            {
                vrCamera.position = player2.position;
            }
        }
    }
}
