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

    //받은 값을 저장해야 한다.
    public Frame(GameObject go, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        pos = position;
        rot = rotation;
        this.scale = scale;
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
    #endregion
}
