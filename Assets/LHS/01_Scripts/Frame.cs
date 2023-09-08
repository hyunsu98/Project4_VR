using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//저장해야하는 값 : GameObject, Position, Rotation, Scale, Animation
public class Frame
{
    Vector3 pos;
    Vector3 scale;
    Quaternion rot;
    GameObject gameObject;

    List<AnimationRecord> animation_record;

    //저장
    public Frame(GameObject go, Vector3 position, Quaternion rotation, Vector3 scale, List<AnimationRecord> anim_records)
    {
        pos = position;
        rot = rotation;
        this.scale = scale;
        animation_record = anim_records;
        gameObject = go;
    }

    #region 값 얻기
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

    //ReplayRecord 에서도 List가 있는데 ?
    public List<AnimationRecord> Animation_Records
    {
        get
        {
            return animation_record;
        }
    }
    #endregion
}
