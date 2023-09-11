using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//카메라
//플레이된 객체의 정보를 List를 통해 담아야 한다.
public class ReplayManager : MonoBehaviour
{
    //플레이 한 동작들의 게임오브젝트를 List에 담아서 저장
    List<ReplayRecord> replay_records;

    //녹화할 최대 길이
    public int max_length = 100;

    public Slider slider;
    bool slider_controlling;

    //카메라 리스트
    int camera_index = 0;
    public List<Camera> kameralar;

    //줌 -> VR은 안됨 
    public float maximum_zoom = 150f; 
    public float minumum_zoom = 10f;
    //얼만큼 줄어들것인지
    public float zoom_index = 5f;
    public float rotation_index = 2f;

    public Canvas cnvs;

    //Start하면 오류 되서 생성자에서?
    public ReplayManager()
    {
        replay_records = new List<ReplayRecord>();

        //시작할때 녹화모드 -> 버튼을 누르면 실행될 수 있게 함
        Game.Game_Mode = Game.Game_Modes.RECORD;

        //처음은 컨트롤러 false;
        slider_controlling = false;
    }

    void Update()
    {
        #region Input 시스템 (카메라)
        //카메라 시점 변경
        if (Input.GetKeyDown(KeyCode.V))
        {
            Camera_Change();
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            Zoom();
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            UnZoom();
        }

        if (Input.GetKey(KeyCode.Q))
        {
            Camera_rot_up();
        }

        if (Input.GetKey(KeyCode.E))
        {
            Camera_rot_down();
        }

        if (Input.GetKey(KeyCode.R))
        {
            Camera_rot_left();
        }

        if (Input.GetKey(KeyCode.T))
        {
            Camera_rot_right();
        }
        #endregion

        // 캔버스
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(cnvs.enabled)
            {
                cnvs.enabled = false;
                Exit();
            }
            else
            {
                cnvs.enabled = true;
                Replay();
            }
        }
      
        //게임모드가 레코드가 아니라면 플레이 가능 (녹화중이 아닐 때)
        if (Game.Game_Mode != Game.Game_Modes.RECORD)
        {
            // 플레이어 리플레이기능 실행 (배열)
            foreach (ReplayRecord item in replay_records)
            {
                //슬라이드 컨트롤러가 true 일때 
                if (slider_controlling)
                {
                    //현재 프레임을 전달해야함.

                    //슬라이더의 값은 int형으로 형변환
                    //item.SetFrame((int)slider.value);
                    item.SetFrame(Convert.ToInt32((slider.value)));
                }

                else
                {
                    //item 프레임을 읽어온다.
                    slider.value = item.GetFrame();
                    //슬라이더의 최대길이는 플레이 레코더의 길이만큼!
                    slider.maxValue = item.Lenght;
                }

                if (Game.Game_Mode == Game.Game_Modes.REPLAY)
                {
                    //다시 처음부터 시작할 수 있게
                    item.SetFrame(-1);
                }

                //나갈때는 길이의 -2을 해준다. //Get_Frame()에서 frame_index++ 해주기 때문에
                if(Game.Game_Mode == Game.Game_Modes.Exit)
                {
                    item.SetFrame(item.Lenght - 2);
                }

                item.Play(); 
            }
        }

        //리플레이 모드는 재생모드로 
        if (Game.Game_Mode == Game.Game_Modes.REPLAY)
        {
            Game.Game_Mode = Game.Game_Modes.PLAY;
        }

        if(Game.Game_Mode == Game.Game_Modes.Exit)
        {
            Game.Game_Mode = Game.Game_Modes.RECORD;
            //시간 멈추기
            Time.timeScale = 1;
        }
    }

    public void Camera_Change()
    {
        //누를때마다 카메라 인덱스 추가 나머지값
        camera_index = (camera_index + 1) % kameralar.Count;

        //카메라 리스트가 null이 아니라면
        if (kameralar != null)
        {
            for (int i = 0; i < kameralar.Count; i++)
            {
                //카메라 키기
                if (i == camera_index)
                {
                    kameralar[i].enabled = true;
                }

                else
                {
                    kameralar[i].enabled = false;
                }
            }
        }
    }

    #region 카메라 -> 직접 손으로 들고 찍는 ..? 
    //카메라 줌기능 VR은 줌 아웃 안됨!
    public void Zoom()
    {
        //min 줌보다는 클때 
        if(kameralar[camera_index].fieldOfView > minumum_zoom)
        {
            kameralar[camera_index].fieldOfView -= zoom_index;
        }
    }

    public void UnZoom()
    {
        //min 줌보다는 클때 
        if (kameralar[camera_index].fieldOfView < maximum_zoom)
        { 
            kameralar[camera_index].fieldOfView += zoom_index;
        }
    }

    //제한각을 걸어놓으면
    public void Camera_rot_up()
    {
        Vector3 rot = kameralar[camera_index].transform.localEulerAngles;
        rot.x = (rot.x + rotation_index) % 360; //360 를 왜 나누는지
        kameralar[camera_index].transform.localEulerAngles = rot;
    }

    public void Camera_rot_down()
    { 
        Vector3 rot = kameralar[camera_index].transform.localEulerAngles;
        rot.x = (rot.x - rotation_index) % 360; //360 를 왜 나누는지
        kameralar[camera_index].transform.localEulerAngles = rot;
    }

    public void Camera_rot_left()
    {
        Vector3 rot = kameralar[camera_index].transform.localEulerAngles;
        rot.y = (rot.y - rotation_index) % 360; //360 를 왜 나누는지
        kameralar[camera_index].transform.localEulerAngles = rot;
    }

    public void Camera_rot_right()
    {
        Vector3 rot = kameralar[camera_index].transform.localEulerAngles;
        rot.y = (rot.y + rotation_index) % 360; //360 를 왜 나누는지
        kameralar[camera_index].transform.localEulerAngles = rot;
    }
    #endregion

    //레코더들을 List에 담기
    public void AddRecord(ReplayRecord rec)
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
        //이전 -> Update문에서 해줘야 함.
        /* Game.Game_Mode = Game.Game_Modes.RECORD;
        //시간 멈추기
        Time.timeScale = 1;*/

        Game.Game_Mode = Game.Game_Modes.Exit;
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
        PLAY,//재생
        PAUSE, //멈춤
        RECORD, //녹음
        REPLAY, //리플레이
        Slider, //슬라이더
        Exit //나가기
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