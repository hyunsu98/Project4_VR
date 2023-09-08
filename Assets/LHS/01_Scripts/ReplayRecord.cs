using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�÷��̾�
public class ReplayRecord : MonoBehaviour
{
    public ReplayOynatici oynatici;
    public Animator animasyon;

    List<Frame> frames;
    List<AnimationRecord> anim_records;

    int max_lenght;
    int length;
    //�ε����� -1���� ������ �� �ְ�
    int frame_index = -1;

    void Start()
    {
        //�����ϸ� oynatici list�� ����
        oynatici.Ekle(this);
        //ó�� ���̴� ī�޶��� max���̿� ���ƾ��Ѵ�.
        max_lenght = oynatici.max_length;
        frames = new List<Frame>();
    }

    void Update()
    {
        if(Game.Game_Mode == Game.Game_Modes.RECORD)
        {
            //�� ��� Update������ �ұ�?
            anim_records = new List<AnimationRecord>();

            //�� �ִϸ��̼� ��������
            //�ִϸ��̼��� null�� �ƴ϶��
            if (animasyon != null)
            {
                //�迭�� �ݺ��� -> ���� �Ķ������ �̸��� �ִ´�.
                foreach (AnimatorControllerParameter item in animasyon.parameters)
                {
                    string name = item.name;

                    //���� ������ Ÿ���� �ִϸ��̼� ��Ʈ�ѷ� �Ķ���� bool�� ������ 
                    if (item.type == AnimatorControllerParameterType.Bool)
                    {
                        //�����ؼ� �־���� �Ѵ�. //item.defaultBool �⺻ bool ��?
                        anim_records.Add(new AnimationRecord(name, animasyon.GetBool(name), item.type));
                    }

                    if (item.type == AnimatorControllerParameterType.Float)
                    {
                        anim_records.Add(new AnimationRecord(name, animasyon.GetFloat(name), item.type));
                    }

                    if (item.type == AnimatorControllerParameterType.Int)
                    {
                        anim_records.Add(new AnimationRecord(name, animasyon.GetInteger(name), item.type));
                    }
                }
            }

            Frame frame = new Frame(this.gameObject, transform.position, transform.rotation, transform.localScale, anim_records);
            Ekle(frame);
        }
    }

    void Ekle(Frame frm)
    {
        //���̰� �ִ���̺��� �۴ٸ�
        if(length < max_lenght)
        {

        }

        else
        {
            //RemoveAt(index) �ش� �ε����� �ִ� ���� ����
            frames.RemoveAt(0);
            length = max_lenght - 1;
        }

        frames.Add(frm);
        length++;
    }

    public void Play()
    {
        Frame frm;

        //frm�� Get_Frame()�϶� null�� �ƴ϶��
        if((frm = Get_Frame()) != null)
        {
            transform.position = frm.Position;
            transform.rotation = frm.Rotation;
            transform.localScale = frm.Scale;

            //�ִϸ��̼�
            foreach(var item in frm.Animation_Records)
            {
                string name = item.Name_;
                
                //����� �ִϸ��̼��� �÷��� �Ѵ�.
                if(item.Type == AnimatorControllerParameterType.Bool)
                {
                    animasyon.SetBool(name, item.Bool_);
                    //���� �ܰ踦 �ߴ�.
                    continue;
                }

                else if (item.Type == AnimatorControllerParameterType.Int)
                {
                    animasyon.SetInteger(name, item.Int_);
                    continue;
                }

                else if (item.Type == AnimatorControllerParameterType.Float)
                {
                    animasyon.SetFloat(name, item.Float_);
                    continue;
                }
            }
        }

        else
        {
            Debug.Log("Replay bitti");
            Game.Game_Mode = Game.Game_Modes.PAUSE;
        }
    }

    //������ �ε����� �߰��Ѵ�.
    Frame Get_Frame()
    {
        frame_index++;

        //������� �����̶��
        if(Game.Game_Mode == Game.Game_Modes.PAUSE)
        {
            frame_index--;
            Time.timeScale = 0;
        }

        else
        {
            Time.timeScale = 1;
        }

        //�������ε����� ���̺��� ũ�ų� ������
        if(frame_index >= length)
        {
            //����
            Game.Game_Mode = Game.Game_Modes.PAUSE;
            frame_index = length - 1;
            //null�� ��ȯ��Ų��
            return null;
        }

        if(frame_index == -1)
        {
            //�̰� �Ⱦ��� ������
            frame_index = length - 1;
        }
        Debug.Log(frame_index + " ," + max_lenght + ","+ length);

        return frames[frame_index];
    }

    public void SetFrame(int value)
    {
        frame_index = value;
    }
    
    //�������ε��� �Ѱ��ֱ�
    public int GetFrame()
    {
        return frame_index;
    }

    //�����̴��� ���� ���̸� �Ѱ���� �Ѵ�.
    public int Lenght
    {
        get
        {
            return length;
        }
    }
}
