using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ī�޶�

//���÷��� �÷��̾�? 
//�÷��̵� ��ü�� ������ List�� ���� ��ƾ� �Ѵ�.
public class ReplayManager : MonoBehaviour
{
    //�÷��� �� ������ List�� ��Ƽ� ����
    List<ReplayRecord> replay_records;
    //��ȭ�� �ִ� ����
    public int max_length = 100;
    public Slider slider;
    bool slider_controlling;

    //ī�޶� ����Ʈ
    int camera_index = 0;
    public List<Camera> kameralar;

    //��
    public float maximum_zoom = 150f; 
    public float minumum_zoom = 10f;
    //��ŭ �پ�������
    public float zoom_index = 5f;
    public float rotation_index = 2f;

    public Canvas cnvs;

    public ReplayManager()
    {
        replay_records = new List<ReplayRecord>();

        //�����Ҷ� ��ȭ���
        Game.Game_Mode = Game.Game_Modes.RECORD;

        //ó���� ��Ʈ�ѷ� false;
        slider_controlling = false;
    }

    void Start()
    {
        //Start�ϸ� ���� �Ǽ� �����ڿ���?
        /*replay_records = new List<ReplayRecord>();

        //�����Ҷ� ��ȭ���
        Game.Game_Mode = Game.Game_Modes.RECORD;*/
    }

    void Update()
    { 
        //ī�޶� ���� ����
        if(Input.GetKeyDown(KeyCode.V))
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("�ٿ�");
            //cnvs.enabled = !cnvs.enabled;
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

        //���Ӹ�尡 ���ڵ尡 �ƴ϶�� �÷��� ���� (��ȭ���� �ƴ϶��)
        if (Game.Game_Mode != Game.Game_Modes.RECORD)
        {
            // �÷��̾� ���÷��̱�� ����
            foreach (ReplayRecord item in replay_records)
            {
                //�����̵� ��Ʈ�ѷ��� true �϶� 
                if (slider_controlling)
                {
                    //�����̴��� ���� int������ ����ȯ
                    //item.SetFrame((int)slider.value);
                    item.SetFrame(Convert.ToInt32((slider.value)));
                }

                else
                {
                    slider.value = item.GetFrame();
                    //�����̴��� �ִ���̴� �÷��� ���ڴ��� ���̸�ŭ!
                    slider.maxValue = item.Lenght;
                }

                if (Game.Game_Mode == Game.Game_Modes.REPLAY)
                {
                    item.SetFrame(-1);
                }

                //�������� ������ -2�� ���ش�. //Get_Frame()���� frame_index++ ���ֱ� ������
                if(Game.Game_Mode == Game.Game_Modes.Exit)
                {
                    item.SetFrame(item.Lenght - 2);
                }

                item.Play(); 
            }
        }

        //���÷��� ���� ������� 
        if (Game.Game_Mode == Game.Game_Modes.REPLAY)
        {
            Game.Game_Mode = Game.Game_Modes.PLAY;
        }

        if(Game.Game_Mode == Game.Game_Modes.Exit)
        {
            Game.Game_Mode = Game.Game_Modes.RECORD;
            //�ð� ���߱�
            Time.timeScale = 1;
        }
    }

    public void Camera_Change()
    {
        //���������� ī�޶� �ε��� �߰� ��������
        camera_index = (camera_index + 1) % kameralar.Count;

        //ī�޶� ����Ʈ�� null�� �ƴ϶��
        if (kameralar != null)
        {
            for (int i = 0; i < kameralar.Count; i++)
            {
                //ī�޶� Ű��
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

    #region ī�޶� -> ���� ������ ��� ��� ..?
    //ī�޶� �ܱ��
    public void Zoom()
    {
        //min �ܺ��ٴ� Ŭ�� 
        if(kameralar[camera_index].fieldOfView > minumum_zoom)
        {
            kameralar[camera_index].fieldOfView -= zoom_index;
        }
    }

    public void UnZoom()
    {
        //min �ܺ��ٴ� Ŭ�� 
        if (kameralar[camera_index].fieldOfView < maximum_zoom)
        { 
            kameralar[camera_index].fieldOfView += zoom_index;
        }
    }

    //���Ѱ��� �ɾ������
    public void Camera_rot_up()
    {
        Vector3 rot = kameralar[camera_index].transform.localEulerAngles;
        rot.x = (rot.x + rotation_index) % 360; //360 �� �� ��������
        kameralar[camera_index].transform.localEulerAngles = rot;
    }

    public void Camera_rot_down()
    { 
        Vector3 rot = kameralar[camera_index].transform.localEulerAngles;
        rot.x = (rot.x - rotation_index) % 360; //360 �� �� ��������
        kameralar[camera_index].transform.localEulerAngles = rot;
    }

    public void Camera_rot_left()
    {
        Vector3 rot = kameralar[camera_index].transform.localEulerAngles;
        rot.y = (rot.y - rotation_index) % 360; //360 �� �� ��������
        kameralar[camera_index].transform.localEulerAngles = rot;
    }

    public void Camera_rot_right()
    {
        Vector3 rot = kameralar[camera_index].transform.localEulerAngles;
        rot.y = (rot.y + rotation_index) % 360; //360 �� �� ��������
        kameralar[camera_index].transform.localEulerAngles = rot;
    }
    #endregion

    public void AddRecord(ReplayRecord rec)
    {
        replay_records.Add(rec);
    }

    //����
    public void Pause()
    {
        Game.Game_Mode = Game.Game_Modes.PAUSE;
        //�ð� ���߱�
        Time.timeScale = 0;
    }

    //�÷���
    public void Play()
    {
        Game.Game_Mode = Game.Game_Modes.PLAY;
        //�ð� ���߱�
        Time.timeScale = 1;
    }

    //���÷���
    public void Replay()
    {
        Game.Game_Mode = Game.Game_Modes.REPLAY;
        //�ð� ���߱�
        Time.timeScale = 1;
    }

    //��ȭ
    public void Exit()
    {
        //���� -> Update������ ����� ��.
        /* Game.Game_Mode = Game.Game_Modes.RECORD;
        //�ð� ���߱�
        Time.timeScale = 1;*/

        Game.Game_Mode = Game.Game_Modes.Exit;
    }

    //�����̴� Ŭ�������� Pointer Down
    public void Click_Slider()
    {
        slider_controlling = true;
    }

    //Pointer Up
    public void Slider_Breack()
    {
        slider_controlling = false;
        //�ٽ� ����ؾ���.
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
        Slider,
        Exit
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