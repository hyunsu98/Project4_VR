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
        // ����ġ���� ���� �չ������� Ray�� �����
        Ray ray = new Ray(hand.position, hand.forward);
        lr.SetPosition(0, hand.position);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            lr.SetPosition(1, hitInfo.point);

            // �ε��� ���� �ִٸ�
            // ���� ���콺 ���ʹ�ư�� ������ ��
            // �س�ȭ���ᰡ ���� �̰� �����ؾ���
            if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.Touch))
            {
                //���� �������� Enemy���
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
