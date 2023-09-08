using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//카메라

//리플레이 플레이어? 
//플레이된 객체의 정보를 List를 통해 담아야 한다.
public class ReplayOynatici : MonoBehaviour
{
    //플레이 한 동작을 List에 담아서 저장
    List<ReplayRecord> replay_records;
    //녹화할 최대 길이
    public int max_length = 100;
    public Slider slider;
    bool slider_controlling;

    public ReplayOynatici()
    {
        replay_records = new List<ReplayRecord>();

        //시작할때 녹화모드
        Game.Game_Mode = Game.Game_Modes.RECORD;

        //처음은 컨트롤러 false;
        slider_controlling = false;
    }

    void Start()
    {
        //Start하면 오류 되서 생성자에서?
        /*replay_records = new List<ReplayRecord>();

        //시작할때 녹화모드
        Game.Game_Mode = Game.Game_Modes.RECORD;*/
    }

    void Update()
    { 
        //게임모드가 레코드가 아니라면 플레이 가능 (녹화중이 아니라면)
        if(Game.Game_Mode != Game.Game_Modes.RECORD)
        {
            // 플레이어 리플레이기능 실행
            foreach (ReplayRecord item in replay_records)
            {
                //슬라이드 컨트롤러가 true 일때 
                if (slider_controlling)
                {
                    //슬라이더의 값은 int형으로 형변환
                    //item.SetFrame((int)slider.value);
                    item.SetFrame(Convert.ToInt32((slider.value)));
                }

                else
                {
                    slider.value = item.GetFrame();
                    //슬라이더의 최대길이는 플레이 레코더의 길이만큼!
                    slider.maxValue = item.Lenght;
                }

                if (Game.Game_Mode == Game.Game_Modes.REPLAY)
                {
                    item.SetFrame(-1);
                }

                item.Play(); 
            }
        }

        if (Game.Game_Mode == Game.Game_Modes.REPLAY)
        {
            Game.Game_Mode = Game.Game_Modes.PLAY;
        }
    }

    public void Ekle(ReplayRecord rec)
    {
        replay_records.Add(rec);
    }

    //정지
    public void Pause()
    {
        Game.Game_Mode = Game.Game_Modes.PAUSE;
        //시간 멈추기
        Time.timeScale = 0;
    }

    //플레이
    public void Play()
    {
        Game.Game_Mode = Game.Game_Modes.PLAY;
        //시간 멈추기
        Time.timeScale = 1;
    }

    //리플레이
    public void Replay()
    {
        Game.Game_Mode = Game.Game_Modes.REPLAY;
        //시간 멈추기
        Time.timeScale = 1;
    }

    //녹화
    public void Exit()
    {
        Game.Game_Mode = Game.Game_Modes.RECORD;
        //시간 멈추기
        Time.timeScale = 1;
    }

    //슬라이더 클릭했을때 Pointer Down
    public void Click_Slider()
    {
        slider_controlling = true;
    }

    //Pointer Up
    public void Slider_Breack()
    {
        slider_controlling = false;
        //다시 재생해야함.
        Play();
    }
}

public static class Game
{
    public static Game_Modes Game_mode;

    public enum Game_Modes
    {
        PLAY,
        PAUSE,
        RECORD,
        REPLAY,
        Slider
    }

    public static Game_Modes Game_Mode
    {
        get
        {
            return Game_mode;
        }

        set
        {
            Game_mode = value;
        }
    }
}