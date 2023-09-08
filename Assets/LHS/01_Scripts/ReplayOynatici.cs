using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ī�޶�

//���÷��� �÷��̾�? 
//�÷��̵� ��ü�� ������ List�� ���� ��ƾ� �Ѵ�.
public class ReplayOynatici : MonoBehaviour
{
    //�÷��� �� ������ List�� ��Ƽ� ����
    List<ReplayRecord> replay_records;
    //��ȭ�� �ִ� ����
    public int max_length = 100;
    public Slider slider;
    bool slider_controlling;

    public ReplayOynatici()
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
        //���Ӹ�尡 ���ڵ尡 �ƴ϶�� �÷��� ���� (��ȭ���� �ƴ϶��)
        if(Game.Game_Mode != Game.Game_Modes.RECORD)
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
        Game.Game_Mode = Game.Game_Modes.RECORD;
        //�ð� ���߱�
        Time.timeScale = 1;
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