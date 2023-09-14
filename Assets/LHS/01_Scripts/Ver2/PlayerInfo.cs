using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

//����Ǿ��� ���� : ������Ʈ�̸�, �ð�, ��ġ, ȸ��, �� ��ġ, ��������
[Serializable]
public class PlayerInfo
{
    //�̸�
    public GameObject gameObject;
    //�ð�
    public float time;
    //��ġ
    public Vector3 pos;
    //ȸ��
    public Quaternion rot;

    //HandAnchor �� ��ġ (���� / ������)
    public LeftHandInfo leftHand;

    public LeftHandInfo rightHand;
}

//����
[Serializable]
public class LeftHandInfo
{
    public Vector3 pos;
    public Quaternion rot;
}

//������
[Serializable]
public class RightHandInfo
{
    public Vector3 pos;
    public Quaternion rot;
}

//��ü�� ��ȭ�� �������� ���� Ŭ����
[Serializable]
public class PlayerJsonList<T>
{
    public List<T> playerJsonList;
}

[Serializable]
public class UnitInfo
{
    //���ӿ�����Ʈ
    public GameObject unit;
    //��ȭ�� ����
    public PlayerJsonList<PlayerInfo> loadInfo;
    //�ε� �� �ε��� ��ȣ
    public int unitLoadIndex;
    //�ε� �� �ð�
    public float curTime;
}

//�� ���� ����