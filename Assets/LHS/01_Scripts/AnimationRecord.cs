using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//매개변수를 저장해야함 float / bool / int / trigger (AnimatorControllerParameter)
public class AnimationRecord
{
    //애니메이션 이름
    string name;

    float Deger_float;
    int Deger_int;
    bool bool_deger;

    //애니메이션 저장
    public AnimationRecord(string n , float deger, AnimatorControllerParameterType type)
    {
        Deger_float = deger;
        name = n;
    }
}
