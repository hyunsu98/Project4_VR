using Oculus.Platform;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerMove : MonoBehaviour
{
    // Player 1 눈 위치
    public Transform trEye;
    // OVR Rig 
    public Transform trOvrRig;
    // CenterEye
    public Transform trCenterEye;
    // Model -> Rig Builder
    public RigBuilder rigBuilder;

    //이동해야 하는 Player
    public Transform targetPlayer;

    public Transform player;

    void Update()
    {
        // 카메라 위치 셋팅
        Vector3 offset = trEye.position - trCenterEye.position;
        trOvrRig.position += offset;

        // Vr 카메라의 위치와 회전을 플레이어에 적용
        //나의 자식으로 있는 플레이어로 계속 바뀌어야 함.
        CharacterModel myPlayer = transform.GetComponentInChildren<CharacterModel>();

        Quaternion newRotation = Quaternion.Euler(0, trCenterEye.rotation.eulerAngles.y, 0);
        myPlayer.transform.rotation = newRotation;

        // 플레이어 교체 코드
        if (Input.GetKeyDown(KeyCode.G))
        {
            //rigBuilder 를 비활성화
            rigBuilder.enabled = false;
            //rigBuilder 를 이용해서 부모로부터 나가자
            rigBuilder.transform.SetParent(null);
            //나의 위치를 targetPlayer 의 위치로 하자
            transform.position = targetPlayer.position;
            //나의 각도를 targetPlayer 의 각도로 하자
            transform.rotation = targetPlayer.rotation;
            //targetPlayer 에서 CharacterModel 를 가져오자.
            CharacterModel cm = targetPlayer.GetComponent<CharacterModel>();
            //trEye 에 가져온 컴포넌트의 trEye 를 셋팅
            trEye = cm.trEye;

            //targetPlayer 에서 RigBuilder 를 가져오자. (지역 변수로 받아라)
            RigBuilder rb = targetPlayer.GetComponent<RigBuilder>();
            //가져온 컴포넌트 를 활성화
            rb.enabled = true;
            //targetPlayer 의 부모를 나로 하자
            targetPlayer.SetParent(transform);
            //targetPlayer 에 rigBuilder 의 transform 을 넣자.
            targetPlayer = rigBuilder.transform;
            //rigBuilder 에 위에 지역변수로 받아놨던 rigBuilder를 셋팅
            rigBuilder = rb;
        }
    }
}
