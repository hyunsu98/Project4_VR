using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform hand;
    public LineRenderer lr;
    public Transform marker;
    public float kAdjust = 1;
    public float maxLineDistance = 3f;

    private bool isPlacingPlayer = false;
    private GameObject spawnObject;
    private Vector3 placementPosition;
    bool isClickPending = false; // Ŭ�� ��� ���¸� ����
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        marker.localScale = Vector3.one * kAdjust;
    }

    void Update()
    {
        if (isPlacingPlayer)
        {
            placementPosition = hand.position + hand.forward * maxLineDistance;

            if (spawnObject != null)
            {
                // �÷��̾��� ��ġ�� ���� ǥ������ ����
                placementPosition.y = GetGroundHeight(placementPosition);
                spawnObject.transform.position = placementPosition;
            }

            if (Input.GetMouseButtonDown(0))
            {
                PlacePlayer();
            }
        }
    }

    public void OnClickSelect(string name)
    {
        Ray ray = new Ray(hand.position, hand.forward);
        lr.SetPosition(0, ray.origin);
        RaycastHit hitInfo;
        bool isHit = Physics.Raycast(ray, out hitInfo, maxLineDistance);

        
        if (isHit)
        {
            lr.SetPosition(1, hitInfo.point);
            marker.position = hitInfo.point;
            marker.up = hitInfo.normal;
            marker.localScale = Vector3.one * kAdjust * hitInfo.distance;

            if (!isPlacingPlayer)
            {
                lr.enabled = true;
                isPlacingPlayer = true;
                placementPosition = hand.position + hand.forward * maxLineDistance;
                if (isHit && hitInfo.collider.CompareTag("Ground"))
                {
                    GameObject tmp = Resources.Load(name) as GameObject;
                    Vector3 spawnPosition = hitInfo.point;


                    // �÷��̾��� ��ġ�� ���� ǥ������ ����
                    spawnPosition.y = GetGroundHeight(spawnPosition);
                    // spawnPosition.y = 0f; // player�� ���� ������ ������ �������� y ���� 0���� ����
                    GameObject obj = Instantiate(tmp, spawnPosition, Quaternion.identity);
               
                    spawnObject = obj;

                    isClickPending = true; // ù ��° Ŭ�� �̺�Ʈ�� �߻��� �� �� ��° Ŭ�� ��� ���·� ����
                    print("�����������");
                }
            }
        }
        else
        {
            lr.enabled = false;
            lr.SetPosition(1, ray.origin + ray.direction * maxLineDistance);
            marker.position = ray.origin + ray.direction * maxLineDistance;
            marker.up = -ray.direction;
            marker.localScale = Vector3.one * kAdjust * maxLineDistance;
        }
    }

    private void PlacePlayer()
    {
        if (spawnObject != null)
        {
            isClickPending = false; // �� ��° Ŭ�� �̺�Ʈ�� ó����
        }
        lr.enabled = false;
        isPlacingPlayer = false;
    }

    private float GetGroundHeight(Vector3 position)
    {
        Ray ray = new Ray(position + Vector3.up * 100f, Vector3.down);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            return hitInfo.point.y;
        }

        return 0f; // ���� ã�� ���� ��� �⺻������ 0�� ��ȯ
    }
}