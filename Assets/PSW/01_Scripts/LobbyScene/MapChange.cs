using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// �� 1�� ������ �� ����
public class MapChange : MonoBehaviour
{
    public OVRInput.Button button;
    public OVRInput.Controller controller;

    public void OnClickMap()
    {
        // Scene PSW_REC2 �� ������ ��
        SceneManager.LoadScene("PSW_Main0925");
    }
}
