using UnityEngine;

public class PlayerChange : MonoBehaviour
{
    public OVRInput.Button button;
    public OVRInput.Controller controller;

    //오른손에서 그려지는 ray
    public Transform hand;
    LineRenderer lr;

    RaycastHit hitInfo;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // 손위치에서 손의 앞방향으로 Ray를 만들고
        Ray ray = new Ray(hand.position, hand.forward);
        lr.SetPosition(0, hand.position);


        if (Physics.Raycast(ray, out hitInfo))
        {
            lr.SetPosition(1, hitInfo.point);

            // 부딪힌 곳이 있다면
            if (OVRInput.GetDown(button, controller))
            {
                if (ModeChange_LHS.instance.isHopln == true)
                {
                    HopIn();
                }
            }
        }

        else
        {
            lr.SetPosition(1, ray.origin + ray.direction * 1000);
        }
    }

    void HopIn()
    {
        //모드 별로 하면 될 것 같음
        //만약 닿은곳이 Enemy라면
        if (hitInfo.collider.CompareTag("Player"))
        {
            Debug.Log(hitInfo.collider.name);
            PlayerMove.instance.CharChange(hitInfo.collider.gameObject);
        }
    }
}
