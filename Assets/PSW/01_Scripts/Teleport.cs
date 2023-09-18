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
    bool isClickPending = false; // 클릭 대기 상태를 추적
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
                // 플레이어의 위치를 땅의 표면으로 조정
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


                    // 플레이어의 위치를 땅의 표면으로 조정
                    spawnPosition.y = GetGroundHeight(spawnPosition);
                    // spawnPosition.y = 0f; // player가 땅에 박히는 현상이 생김으로 y 값을 0으로 고정
                    GameObject obj = Instantiate(tmp, spawnPosition, Quaternion.identity);
               
                    spawnObject = obj;

                    isClickPending = true; // 첫 번째 클릭 이벤트가 발생한 후 두 번째 클릭 대기 상태로 변경
                    print("내려놓고싶음");
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
            isClickPending = false; // 두 번째 클릭 이벤트가 처리됨
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

        return 0f; // 땅을 찾지 못한 경우 기본값으로 0을 반환
    }
}