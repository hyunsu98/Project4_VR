using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // 싱글톤
    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // SFX
    public enum ESfx
    {
        SFX_BUTTON,
    }

    // 효과음 배열
    public AudioClip[] sfxs;

    // 오디오소스
    public AudioSource audioSfx;

    //SFX Play 마우스 클릭 할 곳에서 쓸 코드
    public void PlaySFX(ESfx sfxIdx)
    {
        // 효과음이 끊기지 않고
        audioSfx.PlayOneShot(sfxs[(int)sfxIdx]);
    }
    //SoundManager.instance.PlaySF(SoundManager.ESfx.SFX_BUTTON);
}
