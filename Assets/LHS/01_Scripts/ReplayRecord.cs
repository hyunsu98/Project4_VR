using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어
public class ReplayRecord : MonoBehaviour
{
    public ReplayOynatici oynatici;
    public Animator animasyon;

    List<Frame> frames;
    List<AnimationRecord> anim_records;

    int max_lenght;
    int length;
    //인덱스는 -1부터 시작할 수 있게
    int frame_index = -1;

    void Start()
    {
        //시작하면 oynatici list에 저장
        oynatici.Ekle(this);
        //처음 길이는 카메라의 max길이와 같아야한다.
        max_lenght = oynatici.max_length;
        frames = new List<Frame>();
    }

    void Update()
    {
        if(Game.Game_Mode == Game.Game_Modes.RECORD)
        {
            //왜 얘는 Update문에서 할까?
            anim_records = new List<AnimationRecord>();

            //※ 애니메이션 오류생김
            //애니메이션이 null이 아니라면
            if (animasyon != null)
            {
                //배열의 반복문 -> 현재 파라미터의 이름을 넣는다.
                foreach (AnimatorControllerParameter item in animasyon.parameters)
                {
                    string name = item.name;

                    //만약 아이템 타입이 애니메이션 컨트롤러 파라미터 bool과 같으면 
                    if (item.type == AnimatorControllerParameterType.Bool)
                    {
                        //생성해서 넣어줘야 한다. //item.defaultBool 기본 bool 값?
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
        //길이가 최대길이보다 작다면
        if(length < max_lenght)
        {

        }

        else
        {
            //RemoveAt(index) 해당 인덱스에 있는 것을 제거
            frames.RemoveAt(0);
            length = max_lenght - 1;
        }

        frames.Add(frm);
        length++;
    }

    public void Play()
    {
        Frame frm;

        //frm이 Get_Frame()일때 null이 아니라면
        if((frm = Get_Frame()) != null)
        {
            transform.position = frm.Position;
            transform.rotation = frm.Rotation;
            transform.localScale = frm.Scale;

            //애니메이션
            foreach(var item in frm.Animation_Records)
            {
                string name = item.Name_;
                
                //저장된 애니메이션을 플레이 한다.
                if(item.Type == AnimatorControllerParameterType.Bool)
                {
                    animasyon.SetBool(name, item.Bool_);
                    //현재 단계를 중단.
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

    //프레임 인덱스를 추가한다.
    Frame Get_Frame()
    {
        frame_index++;

        //만약게임 멈춤이라면
        if(Game.Game_Mode == Game.Game_Modes.PAUSE)
        {
            frame_index--;
            Time.timeScale = 0;
        }

        else
        {
            Time.timeScale = 1;
        }

        //프레임인덱스가 길이보다 크거나 같으면
        if(frame_index >= length)
        {
            //멈춤
            Game.Game_Mode = Game.Game_Modes.PAUSE;
            frame_index = length - 1;
            //null을 반환시킨다
            return null;
        }

        if(frame_index == -1)
        {
            //이거 안쓰면 오류남
            frame_index = length - 1;
        }
        Debug.Log(frame_index + " ," + max_lenght + ","+ length);

        return frames[frame_index];
    }

    public void SetFrame(int value)
    {
        frame_index = value;
    }
    
    //프레임인덱스 넘겨주기
    public int GetFrame()
    {
        return frame_index;
    }

    //슬라이더를 위해 길이를 넘겨줘야 한다.
    public int Lenght
    {
        get
        {
            return length;
        }
    }
}
