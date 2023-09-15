using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//리플레이 기능
public class ReplaySet : MonoBehaviour
{
    public static ReplaySet instance;

    //ReplayInfo<GameObject> replayInfo;
    //게임오브젝트
    public List<GameObject> unit;

    public bool isPlay;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        // saveModel 파일 유무로 플레이어들을 등록해야함.
    }

    // 플레이어 담겨져 있는 변수!!

    // 담겨져 있는 리플레이를 모두 실행! -> 리플레이 클래스에서 
    public void OnRecordPlay()
    {
        if (unit.Count > 0)
        {
            //저장된 객체의 함수를 반복해서 부른다.!
            for (int i = 0; i < unit.Count; i++)
            {
                var playerRecored = unit[i].GetComponent<PlayerRecord>();
                playerRecored.OnRecordPlay();
                isPlay = false;
            }

            print(unit.Count);
        }

        //만약 없다면 재생될 플레이어가 없다고 표시
        else
        {
            print("재생할 게 없어.");
        }
    }

    public void OnAutoReplayForRecording(PlayerRecord who)
    {
        if (unit.Count > 0)
        {
            //저장된 객체의 함수를 반복해서 부른다.!
            for (int i = 0; i < unit.Count; i++)
            {
                var playerRecored = unit[i].GetComponent<PlayerRecord>();
                if (who == playerRecored)
                    continue;
                playerRecored.OnRecordPlay();
                isPlay = false;
            }

            print(unit.Count);
        }

        //만약 없다면 재생될 플레이어가 없다고 표시
        else
        {
            print("재생할 게 없어.");
        }
    }
}
