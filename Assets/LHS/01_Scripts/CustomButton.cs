using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : Button
{
    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        
    }

    public override void OnMove(AxisEventData eventData)
    {
        base.OnMove(eventData);
        print("OnMove");
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        print("OnPointerDown");
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        print("OnPointerEnter");

        transform.localScale = Vector3.one * 1.25f;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        print("OnPointerExit");
        transform.localScale = Vector3.one;
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        print("OnPointerUp");
    }
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        print("OnSelect");
        SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_BUTTON);
    }

    /*private void OnTriggerEnter(Collider other)
    {
        print("´ê¾Òµû´Ù¾Ó¤¡");
        transform.localScale = Vector3.one;
    }

    private void OnTriggerExit(Collider other)
    {
        transform.localScale = Vector3.one;
        print("³ª°«´Ù");
    }*/
}
