using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�Ű������� �����ؾ��� float / bool / int / trigger (AnimatorControllerParameter)
public class AnimationRecord
{
    //�ִϸ��̼� �̸�
    string name;

    float Deger_float;
    int Deger_int;
    bool bool_deger;

    //�ִϸ��̼� ����
    public AnimationRecord(string n , float deger, AnimatorControllerParameterType type)
    {
        Deger_float = deger;
        name = n;
    }
}
