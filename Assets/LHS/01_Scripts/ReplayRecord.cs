using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�÷��̾�
public class ReplayRecord : MonoBehaviour
{
    //���÷��� �Ŵ���
    public ReplayManager replayManager;
    [SerializeField] string replayManagerName = "ReplayManager"; //(���ĺ���)

    //�� ��� �� ��Ȱ��ȭ �� �ʿ䰡 ���� ��ũ��Ʈ - ������ �ʿ��� ����!

    //����� �ִϸ����� , �� ������ �ٵ�, ����� �ҽ� 
    [SerializeField] Animator anim;
    
    //������ ������ ��� (��ü, �ִϸ��̼�)
    List<Frame> frames;
    List<AnimationRecord> anim_records;

    //��������
    int max_lenght;
    //��ȭ�� ����
    int length;

    //�� �������� ������ -1�� ����, �����Ǹ� ������ �������� ���
    //���� ������
    int frame_index = -1;

    void Start()
    {
        //�������� �ֱ�
        if(replayManager == null)
        {
            //replayManager = Camera.main.GetComponent<ReplayManager>();
            replayManager = GameObject.Find(replayManagerName).GetComponent<ReplayManager>(); //(���ĺ���)
        }

        anim = GetComponentInChildren<Animator>();

        if(replayManager != null) //-> ���߿� ��ȭ ��ư ��������
        {
            //�����ϸ� oynatici list�� ����
            replayManager.AddRecord(this);

            //ó�� ���̴� ī�޶��� max���̿� ���ƾ��Ѵ�. (�� ���̴� ������ max ���̶� ����!)
            max_lenght = replayManager.max_length;

            //�����Ӹ��� ����
            frames = new List<Frame>();
        }
    }

    void Update()
    {
        //��ȭ���
        if(Game.Game_Mode == Game.Game_Modes.RECORD)
        {
            //�� ��� Update������ �ұ�?
            anim_records = new List<AnimationRecord>();

            #region �ִϸ��̼�
            //�� �ִϸ��̼� ��������
            //�ִϸ��̼��� null�� �ƴ϶��
            /*if (anim != null)
            {
                //�� �迭�� �ݺ��� -> ���� �Ķ������ �̸��� �ִ´�.
                foreach (AnimatorControllerParameter item in anim.parameters)
                {
                    string name = item.name;

                    //���� ������ Ÿ���� �ִϸ��̼� ��Ʈ�ѷ� �Ķ���� bool�� ������ 
                    if (item.type == AnimatorControllerParameterType.Bool)
                    {
                        //�����ؼ� �־���� �Ѵ�. //item.defaultBool �⺻ bool ��?
                        anim_records.Add(new AnimationRecord(name, anim.GetBool(name), item.type));
                    }

                    if (item.type == AnimatorControllerParameterType.Float)
                    {
                        anim_records.Add(new AnimationRecord(name, anim.GetFloat(name), item.type));
                    }

                    if (item.type == AnimatorControllerParameterType.Int)
                    {
                        anim_records.Add(new AnimationRecord(name, anim.GetInteger(name), item.type));
                    }
                }
            }*/
            #endregion

            Frame frame = new Frame(this.gameObject, transform.position, transform.rotation, transform.localScale, anim_records);
            AddFrame(frame);

            Debug.Log("��ȭ���Դϴ�" + frame);
        }
    }

    //��ȭ
    //���1. ���̰� �������� ��ȭ, �ƴ� ����
    //���2. ó�� ���� ���� �� length���̸� �ϳ� �پ��� ��
    void AddFrame(Frame frm)
    {
        //���̰� �ִ���̺��� �۴ٸ�
        if(length < max_lenght)
        {
            //�������� �����ش�
            frames.Add(frm);
            //�� ���̸� �����ش�.
            length++;
        }

        //���̰� max_lenght���� ũ�ٸ�
        else
        {
            //RemoveAt(index) �ش� �ε����� �ִ� ���� ����
            //��ȭ���� -> UI
            Debug.Log("��ȭ ����");

            //�ٽ� 3��Ī �������� �ٲ��� ��.

            //���2
            /*frames.RemoveAt(0);
            length = max_lenght - 1;*/
        }

        /*//���2 �������� �����ش� 
        frames.Add(frm);
        //�� ���̸� �����ش�.
        length++;*/
    }

    //2 >> ��� �÷���
    public void Play()
    {
        Frame frm;

        //frm�� Get_Frame()�϶� null�� �ƴ϶��
        //frm = Get_Frame() -> ���� �������� ��ġ
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
                    anim.SetBool(name, item.Bool_);
                    //���� �ܰ踦 �ߴ�.
                    continue;
                }

                else if (item.Type == AnimatorControllerParameterType.Int)
                {
                    anim.SetInteger(name, item.Int_);
                    continue;
                }

                else if (item.Type == AnimatorControllerParameterType.Float)
                {
                    anim.SetFloat(name, item.Float_);
                    continue;
                }
            }
        }

        else
        {
            Debug.Log("����� ������ �����ϴ�.");
            Game.Game_Mode = Game.Game_Modes.PAUSE;
        }
    }

    //1 >> ���÷���
    Frame Get_Frame()
    {
        Debug.Log("���÷��� ����");

        frame_index++;

        //������� �����̶��
        if (Game.Game_Mode == Game.Game_Modes.PAUSE)
        {
            //�� ���� �����ӿ��� ������ �� �ֵ���
            frame_index--;

            Debug.Log("����");
            //Time.timeScale = 0;
        }

        else
        {
            Time.timeScale = 1;
            Debug.Log("���");
        }

        //�������ε����� ���̺��� ũ�ų� ������
        if (frame_index >= length)
        {
            //����
            Game.Game_Mode = Game.Game_Modes.PAUSE;
            frame_index = length - 1;
            //null�� ��ȯ��Ų��
            Debug.Log("������ ŭ");
            return null;
        }

        //�����ϱ� ������ �����Ӱ� ��ȭ�� ���̵� -1 -> 0���� �ٽ� ������ �� �ְ�
        if (frame_index == -1)
        {
            //�̰� �Ⱦ��� ������
            frame_index = length - 1;
            Debug.Log("������ ����");
            return null;
        }

        Debug.Log($"���� ������ {frame_index} / �ִ� ���� {max_lenght} / ��ȭ�� ���� {length}");
    

        return frames[frame_index];
    }

    //���� ������ ���� ���
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
        get { return length; }
    }
}
