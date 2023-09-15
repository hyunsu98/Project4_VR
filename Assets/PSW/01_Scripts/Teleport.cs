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
    // ��Ŀ ũ�� �����ϴ� ���� ����
    public float kAdjust = 1;


    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        // ��Ŀ ũ�⸦ �����ϱ� ���� �Ÿ��� ���� ����
        marker.localScale = Vector3.one * kAdjust;
    }

    // Update is called once per frame
    void Update()
    {
        // �ڵ��� ��ġ���� �ڵ��� �չ������� Ray�� �߻��ϰ�
        Ray ray = new Ray(hand.position, hand.forward);
        lr.SetPosition(0, ray.origin);
        RaycastHit hitInfo;
        bool isHit = Physics.Raycast(ray, out hitInfo);
        // �ε��� ���� �ִٸ� �÷��̾ �� ������ ��ġ�ϰ� �ʹ�.
        if (isHit)
        {
            // ��� �ε�����.
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
            // ��� �ε���
            lr.enabled = true;
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            // ���� ���� �� ���� �ʹ�.
            lr.enabled = false;
            // ���� ���� ���� �ִٸ�?
            if(isHit)
            {
                // ���� ���� ���� "Ground" �±װ� �ִٸ� �� ��ġ�� �ν��Ͻ�ȭ �ϰ�ʹ�.
                if (hitInfo.collider.CompareTag("Ground"))
                {
                    Instantiate(player, hitInfo.point, Quaternion.identity);
                    print("�����������");
                    //Instantiate(gameObject, hitInfo.collider.transform);
                }
            }
        }

        //if(Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    Instantiate(player, transform.position, Quaternion.identity);
        //    print("�����������");
        //}
    }
}
