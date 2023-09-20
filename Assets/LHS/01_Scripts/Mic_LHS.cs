using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

// �����ؼ� ���Ϸ� ���� // ���ӿ�����Ʈ�̸� , ���� (�ð���?)
// �ش� ���ӿ�����Ʈ�� �ҷ��;��� -> ���÷��� ����� ���� ��� -> ����� �ҽ��� ����
// ��Ҹ� ������ ���缭 �� ������ �����ϰ� 
public class Mic_LHS : MonoBehaviour
{
    //��������
    AudioClip recording = null;
    //���������̸�
    string microphoneID = null;
    //������ ����
    int recordingLengthSec = 15;
    //���� �ӵ�
    int recordingHZ = 22050;

    public void OnStart()
    {
        print("���� ���� ��");
        PointerDown();
    }

    public void OnEnd()
    {
        print("���� ���� ����");
        PointerUp();
    }

    public void OnReplay()
    {
        //������ �ִٸ�!!!

        StartCoroutine(GetWav2AudioClip(Application.dataPath + "/StreamingAssets/" + gameObject.name + ".wav"));
    }

    //�ε��ϴ� ������ �ѹ��� �ҷ��ͼ� �鸮�� �ؾ��Ѵ�. (�׼� or ����Ʈ�� ��� �ѹ��� PlayOneShot)
    IEnumerator GetWav2AudioClip(string path)
    {
        Uri voiceURI = new Uri(path); //������ ��η�

        UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(voiceURI, AudioType.WAV);

        yield return www.SendWebRequest();

        // ���� ����!
        if(www.result == UnityWebRequest.Result.ConnectionError)
        {
            print(www.error);
        }

        else
        {
            AudioClip clipData = ((DownloadHandlerAudioClip)www.downloadHandler).audioClip;

            AudioSource audio = GetComponent<AudioSource>();

            if (audio)
            {
                audio.clip = clipData;
                audio.Play();
            }
        }
    }

    public void PointerDown()
    { 
        StartRecording();
    }

    public void PointerUp()
    {
        //recorder.TransmitEnabled = false;
        //this.recorder.TransmitEnabled = false;
        StopRecording();

    }

    //��ȭ ����
    public void StartRecording()
    {
        Debug.Log("��ȭ ����");

        //��ġ �̸�, lengthSec�� ������ ��� ������ ����ϰ� AudioClip�� ���� �κк��� ��ȯ�Ͽ� �����ؾ��ϴ� ���� ��Ÿ��
        //
        recording = Microphone.Start(microphoneID, false, recordingLengthSec, recordingHZ);
    }

    //��ȭ ����
    public void StopRecording()
    {
        //��ȭ������ ���ڵ� ���̸� 
        if(Microphone.IsRecording(microphoneID))
        {
            if(recording == null)
            {
                Debug.LogError("���� ������ ����");
                return;
            }

            // ������ ���� ���� ��������
            int lastTime = Microphone.GetPosition(null);

            Microphone.End(microphoneID);
            Debug.Log("���� ����");

            //������ ���ø� �迭�� ���
            float[] sampleData = new float[recording.samples];
            recording.GetData(sampleData, 0);

            float[] cutData = new float[lastTime];

            //sampleData�� ��� ���� lasTime ��ŭ�� cutData�� �����ϱ�
            //array.copy(cutData, sampleData, lastTime -1);
            for(int i = 0; i < cutData.Length; i++)
            {
                cutData[i] = sampleData[i];
            }

            AudioClip newClip = AudioClip.Create("temp", lastTime, recording.channels, recording.frequency, false);
            newClip.SetData(cutData, 0);

            // ���� �ð��� ���ϱ�
            DateTime dt = DateTime.Now;

            //����� ����
            //SavWav_LHS.Save(Application.streamingAssetsPath + "/" + gameObject.name, newClip);
            SavWav_LHS.Save(Application.streamingAssetsPath + "/" + gameObject.name, newClip);
        }
    }

    //���÷��� ����
    //����������� �׳� �������� �Ǵ� �� �ƴѰ�?
    public void ReplayVoice()
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (audio)
        {
            audio.clip = Resources.Load(gameObject.name) as AudioClip;
            audio.Play();
        }
    }
}
