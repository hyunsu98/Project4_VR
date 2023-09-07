using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����ؾ��ϴ� �� : GameObject, Position, Rotation, Scale, Animation
public class Frame
{
    Vector3 pos;
    Vector3 scale;
    Quaternion rot;
    GameObject gameObject;

    //���� ���� �����ؾ� �Ѵ�.
    public Frame(GameObject go, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        pos = position;
        rot = rotation;
        this.scale = scale;
        gameObject = go;
    }

    #region �� ���
    public Vector3 Position
    {
        get
        {
            return pos;
        }
    }

    public Vector3 Scale
    {
        get
        {
            return scale;
        }
    }

    public Quaternion Rotation
    {
        get
        {
            return rot;
        }
    }

    public GameObject GameObject
    {
        get
        {
            return gameObject;
        }
    }
    #endregion
}
