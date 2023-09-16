using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//구현목표
//재생 될 리스트를 담고 플레이 하면 그 객체의 플레이 함수가 실행
//내가 녹화 버튼을 눌렀을 때 재생이 저장된 객체가 있다면
//플레이 될 수 있게
//나는 녹화 //너는 플레이 해 !!!!

//녹화 기능
public class RecSet : MonoBehaviour
{
    // 재생 될 리스트 
    [SerializeField]
    PlayerRecord recrod;

    //재생을 눌렀을때 
    //저장된 플레이가 있다면 재생시키고
    //저장되지 않았으면 녹화시킨다.

    //녹화 시작
    //자식 객체의 player

    public void Update()
    {
      
    }

    public void OnRecordStart()
    {
        //자식에 붙어있는 플레이어의 녹화컴포넌트를 가져온다.
        recrod = transform.GetComponentInChildren<PlayerRecord>();

        //recrod가 null이 아닐때만
        if (recrod != null)
        {
            //만약 담겨져 있는 리플레이 객체가 있다면 재생시키면서 
            //중요! 리플레이가 될때 녹화가 진행되야함.

            Debug.Log("RM" + recrod + "의 녹화시작");
            //녹화 플레이를 재생 시킴!
            recrod.OnRecordStart();
        }

        else
        {
            print("녹화불가능 플레이어");
        }
    }

    public void OnRecordEnd()
    {
        if (recrod != null)
        {
            Debug.Log("RM" + recrod + "의 녹화종료");
            recrod.OnRecordEnd();

            //녹화 정지를 누르면 main 플레이어로 바꿔야 함!
        }

        else
        {
            print("녹화종료할게 없음");
        }
    }
}
