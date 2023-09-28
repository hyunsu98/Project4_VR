using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // �̱���
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

    // ȿ���� �迭
    public AudioClip[] sfxs;

    // ������ҽ�
    public AudioSource audioSfx;

    //SFX Play ���콺 Ŭ�� �� ������ �� �ڵ�
    public void PlaySFX(ESfx sfxIdx)
    {
        // ȿ������ ������ �ʰ�
        audioSfx.PlayOneShot(sfxs[(int)sfxIdx]);
    }
    //SoundManager.instance.PlaySF(SoundManager.ESfx.SFX_BUTTON);
}
