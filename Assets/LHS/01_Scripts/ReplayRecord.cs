using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어
public class ReplayRecord : MonoBehaviour
{
    //리플레이 매니저
    public ReplayManager replayManager;
    [SerializeField] string replayManagerName = "ReplayManager"; //(추후변경)

    //※ 재생 중 비활성화 할 필요가 없는 스크립트 - 움직임 필요할 수도!

    //재생될 애니메이터 , ※ 리지드 바디, 오디오 소스 
    [SerializeField] Animator anim;
    
    //녹음된 프레임 목록 (전체, 애니메이션)
    List<Frame> frames;
    List<AnimationRecord> anim_records;

    //최종길이
    int max_lenght;
    //녹화된 길이
    int length;

    //※ 삭제되지 않으면 -1로 유지, 삭제되면 삭제된 프레임을 사용
    //현재 프레임
    int frame_index = -1;

    void Start()
    {
        //동적으로 넣기
        if(replayManager == null)
        {
            //replayManager = Camera.main.GetComponent<ReplayManager>();
            replayManager = GameObject.Find(replayManagerName).GetComponent<ReplayManager>(); //(추후변경)
        }

        anim = GetComponentInChildren<Animator>();

        if(replayManager != null) //-> 나중에 녹화 버튼 눌렀을때
        {
            //시작하면 oynatici list에 저장
            replayManager.AddRecord(this);

            //처음 길이는 카메라의 max길이와 같아야한다. (내 길이는 설정한 max 길이랑 같게!)
            max_lenght = replayManager.max_length;

            //프레임마다 저장
            frames = new List<Frame>();
        }
    }

    void Update()
    {
        //녹화모드
        if(Game.Game_Mode == Game.Game_Modes.RECORD)
        {
            //왜 얘는 Update문에서 할까?
            anim_records = new List<AnimationRecord>();

            #region 애니메이션
            //※ 애니메이션 오류생김
            //애니메이션이 null이 아니라면
            /*if (anim != null)
            {
                //※ 배열의 반복문 -> 현재 파라미터의 이름을 넣는다.
                foreach (AnimatorControllerParameter item in anim.parameters)
                {
                    string name = item.name;

                    //만약 아이템 타입이 애니메이션 컨트롤러 파라미터 bool과 같으면 
                    if (item.type == AnimatorControllerParameterType.Bool)
                    {
                        //생성해서 넣어줘야 한다. //item.defaultBool 기본 bool 값?
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

            Debug.Log("녹화중입니다" + frame);
        }
    }

    //녹화
    //방법1. 길이가 작을때만 녹화, 아님 종료
    //방법2. 처음 값을 삭제 후 length길이를 하나 줄어들게 함
    void AddFrame(Frame frm)
    {
        //길이가 최대길이보다 작다면
        if(length < max_lenght)
        {
            //프레임을 더해준다
            frames.Add(frm);
            //내 길이를 더해준다.
            length++;
        }

        //길이가 max_lenght보다 크다면
        else
        {
            //RemoveAt(index) 해당 인덱스에 있는 것을 제거
            //녹화종료 -> UI
            Debug.Log("녹화 종료");

            //다시 3인칭 시점으로 바뀌어야 함.

            //방법2
            /*frames.RemoveAt(0);
            length = max_lenght - 1;*/
        }

        /*//방법2 프레임을 더해준다 
        frames.Add(frm);
        //내 길이를 더해준다.
        length++;*/
    }

    //2 >> 재생 플레이
    public void Play()
    {
        Frame frm;

        //frm이 Get_Frame()일때 null이 아니라면
        //frm = Get_Frame() -> 현재 프레임의 위치
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
                    anim.SetBool(name, item.Bool_);
                    //현재 단계를 중단.
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
            Debug.Log("저장된 파일이 없습니다.");
            Game.Game_Mode = Game.Game_Modes.PAUSE;
        }
    }

    //1 >> 리플레이
    Frame Get_Frame()
    {
        Debug.Log("리플레이 시작");

        frame_index++;

        //만약게임 멈춤이라면
        if (Game.Game_Mode == Game.Game_Modes.PAUSE)
        {
            //그 다음 프레임에서 시작할 수 있도록
            frame_index--;

            Debug.Log("멈춤");
            //Time.timeScale = 0;
        }

        else
        {
            Time.timeScale = 1;
            Debug.Log("재생");
        }

        //프레임인덱스가 길이보다 크거나 같으면
        if (frame_index >= length)
        {
            //멈춤
            Game.Game_Mode = Game.Game_Modes.PAUSE;
            frame_index = length - 1;
            //null을 반환시킨다
            Debug.Log("프레임 큼");
            return null;
        }

        //시작하기 전에는 프레임과 녹화된 길이도 -1 -> 0에서 다시 시작할 수 있게
        if (frame_index == -1)
        {
            //이거 안쓰면 오류남
            frame_index = length - 1;
            Debug.Log("프레임 작음");
            return null;
        }

        Debug.Log($"현재 프레임 {frame_index} / 최대 길이 {max_lenght} / 녹화된 길이 {length}");
    

        return frames[frame_index];
    }

    //현재 프레임 쓰고 얻기
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
        get { return length; }
    }
}
