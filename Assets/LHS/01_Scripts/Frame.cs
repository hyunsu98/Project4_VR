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

    List<AnimationRecord> animation_record;

    //����
    public Frame(GameObject go, Vector3 position, Quaternion rotation, Vector3 scale, List<AnimationRecord> anim_records)
    {
        pos = position;
        rot = rotation;
        this.scale = scale;
        animation_record = anim_records;
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

    //ReplayRecord ������ List�� �ִµ� ?
    public List<AnimationRecord> Animation_Records
    {
        get
        {
            return animation_record;
        }
    }
    #endregion
}
