using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChange : MonoBehaviour
{
    public Transform hand;
    LineRenderer lr;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // 손위치에서 손의 앞방향으로 Ray를 만들고
        Ray ray = new Ray(hand.position, hand.forward);
        lr.SetPosition(0, hand.position);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            lr.SetPosition(1, hitInfo.point);

            // 부딪힌 곳이 있다면
            // 만약 마우스 왼쪽버튼을 눌렀을 때
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                //만약 닿은곳이 Enemy라면
                if (hitInfo.collider.CompareTag("Player"))
                {
                    Debug.Log(hitInfo.collider.name);

                    PlayerMove.instance.CharChange(hitInfo.collider.gameObject);
                }
            }
        }

        else
        {
            lr.SetPosition(1, ray.origin + ray.direction * 1000);
        }
    }
    }
