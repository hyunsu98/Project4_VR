using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform hand;
    public GameObject player;
    LineRenderer lr;
    public Transform marker;
    // 마커 크기 조절하는 공식 변수
    public float kAdjust = 1;


    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        // 마커 크기를 조정하기 위해 거리를 곱한 공식
        marker.localScale = Vector3.one * kAdjust;
    }

    // Update is called once per frame
    void Update()
    {
        // 핸드의 위치에서 핸드의 앞방향으로 Ray를 발사하고
        Ray ray = new Ray(hand.position, hand.forward);
        lr.SetPosition(0, ray.origin);
        RaycastHit hitInfo;
        bool isHit = Physics.Raycast(ray, out hitInfo);
        // 부딪힌 곳이 있다면 플레이어를 그 곳으로 배치하고 싶다.
        if (isHit)
        {
            // 어딘가 부딪혔다.
            lr.SetPosition(1, hitInfo.point);
            marker.position = hitInfo.point;
            marker.up = hitInfo.normal;
            marker.localScale = Vector3.one * kAdjust * hitInfo.distance;
        }
        else
        {
            lr.SetPosition(1, ray.origin + ray.direction * 100);
            marker.position = ray.origin + ray.direction * 100;
            marker.up = -ray.direction;
            marker.localScale = Vector3.one * kAdjust * 100;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            // 어딘가 부딪힘
            lr.enabled = true;
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            // 떼면 선을 안 보고 싶다.
            lr.enabled = false;
            // 만약 닿은 곳이 있다면?
            if(isHit)
            {
                // 만약 닿은 곳에 "Ground" 태그가 있다면 그 위치로 인스턴스화 하고싶다.
                if (hitInfo.collider.CompareTag("Ground"))
                {
                    Instantiate(player, hitInfo.point, Quaternion.identity);
                    print("내려놓고싶음");
                    //Instantiate(gameObject, hitInfo.collider.transform);
                }
            }
        }

        //if(Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    Instantiate(player, transform.position, Quaternion.identity);
        //    print("내려놓고싶음");
        //}
    }
}
