﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TK3DController : MonoBehaviour
{
    public GameObject Boys;
    public GameObject Girls;
    public bool Boy;
    public bool Girl;
    public float walkspeed;
    public float runspeed;
    public float sprintspeed;
    public float jumpforce;
    GameObject Character;
    GameObject Model;
    GameObject Helper;
    GameObject Target;
    Transform trans;
    Rigidbody rigid;
    Animator anim;
    Vector3 InputMoveDir;
    float divergence;
    float tospeed;
    float speed;
    float Aspeed;
    float express;
    float grtime;
    bool jumping;
    bool grounded;
    Vector3 dirforw;
    Vector3 dirside;
    float angleforward;
    float angleright;
    GameObject currentGroundObject;
    int collisionCount;
    RaycastHit hit;
    Vector3 targetpoint;
    bool active = true;
    
    float moveAUX;

    void Start()
    {
        if (!Boy && !Girl) Boy = true;
        if (Boy && !Girl) Character = Boys;
        if (Girl && !Boy) Character = Girls;
        if (Girl && Boy)
        {
            int AUX = Random.Range(0, 10);
            if (AUX < 5) Character = Boys;
            else Character = Girls;
        }        

        Helper = new GameObject("Helper");
        Target = new GameObject("Target");

        Model = Instantiate(Character, transform.position, transform.rotation, transform);
        Helper.transform.position = transform.position;
        Helper.transform.parent = transform;
        Target.transform.position = transform.position + new Vector3(0f, 1f, 0f);
        Target.transform.parent = transform;
        Camera.main.transform.position = transform.position + new Vector3(0f, 2f, -3f);
        Camera.main.transform.parent = Helper.transform;
        Camera.main.transform.LookAt(Target.transform);

        trans = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();
        anim = Model.GetComponent<Animator>();
        express = 0f;
        InputMoveDir = transform.forward;
        Helper.transform.Rotate(Vector3.up * 180);
        grounded = true;
    }


    void Update()
    {
        if (active) MoveChar();
        GetInput();
        if (!grounded) grtime += 0.1f;
        else { grtime = 0f; anim.SetBool("Grounded", true); }
        if (grtime > 1.45f) anim.SetBool("Grounded", false);
    }

    private bool IsFloor(Vector3 v)
    {
        float angle = Vector3.Angle(Vector3.up, v);
        return angle < 45f;
    }
    private void OnCollisionStay(Collision collisionInfo)
    {
        foreach (ContactPoint contact in collisionInfo.contacts)
        {
            if (IsFloor(contact.normal))
            {
                if (!jumping) grounded = true;
                currentGroundObject = contact.otherCollider.gameObject;
                return;
            }
            else if (currentGroundObject == contact.otherCollider.gameObject)
            {
                grounded = false;
                currentGroundObject = null;
            }
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject == currentGroundObject)
        {
            grounded = false;
            currentGroundObject = null;
        }
    }

    void Setdir() 
    {
        RaycastHit hit; RaycastHit hit1;

        Physics.Raycast(trans.position + new Vector3(0f, 0.2f, 0f), Vector3.down, out hit);
        Physics.Raycast(trans.position + new Vector3(0f, 0.2f, 0f) + InputMoveDir * 0.25f, Vector3.down, out hit1);

        dirforw = Vector3.Slerp(dirforw, -Vector3.Cross(hit.normal + hit1.normal, Model.transform.right), 18f * Time.deltaTime).normalized;

        angleforward = Vector3.SignedAngle(Model.transform.forward, dirforw, Model.transform.right);
        angleright = Mathf.Lerp(angleright, Vector3.SignedAngle(Model.transform.forward, InputMoveDir, Vector3.up) * 0.125f, 12f * Time.deltaTime);

        anim.SetFloat("Turn", angleright);
        anim.SetFloat("Angle", angleforward);
    }
    void GetInput()
    {        
        //Camera position
        Target.transform.position += new Vector3(0f, Input.GetAxis("Mouse ScrollWheel") * 0.1f, 0f);
        Camera.main.transform.RotateAround(Target.transform.position, -Camera.main.transform.right, Input.GetAxis("Mouse Y"));
        Camera.main.transform.position += Camera.main.transform.forward * Input.GetAxis("Mouse ScrollWheel");
        Camera.main.transform.LookAt(Target.transform.position - Vector3.up * 0.5f);
        Helper.transform.Rotate(0f, Input.GetAxis("Mouse X"), 0f);

        //walk run sprint
        if (active)
        {
            if (Input.GetButtonDown("Jump") && grounded) StartCoroutine("Jump");
            if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
            {
                InputMoveDir = (Helper.transform.forward * Input.GetAxis("Vertical") + Helper.transform.right * Input.GetAxis("Horizontal")).normalized;
                if (Input.GetKey("left shift"))
                {
                    tospeed = runspeed + express * (sprintspeed - runspeed);
                    Aspeed = Mathf.Lerp(Aspeed, 2f + express, 2f * Time.deltaTime);
                }
                else
                {
                    tospeed = walkspeed;
                    Aspeed = Mathf.Lerp(Aspeed, 1f, 2f * Time.deltaTime);
                }
                if (Input.GetKeyDown("left shift") && speed > walkspeed + 0.5f)
                {
                    express = 1f;
                }
                if (Input.GetKeyUp("left shift")) express = 0f;
            }
            else if (Mathf.Abs(Input.GetAxis("Vertical")) < 1f && Mathf.Abs(Input.GetAxis("Horizontal")) < 1f)
            {
                tospeed = 0f;
                if (jumping) Aspeed = 0f;
                Aspeed = Mathf.Lerp(Aspeed, 0f, 2.5f * Time.deltaTime);
            }
        }
        speed = Mathf.Lerp(speed, tospeed, 2.5f * Time.deltaTime);
        anim.SetFloat("Aspeed", Aspeed);
        divergence = Mathf.Abs(Vector3.SignedAngle(Model.transform.forward, InputMoveDir, Vector3.up));
        Setdir();

        Debug.DrawRay(transform.position + new Vector3(0f, 1.45f, 0f), InputMoveDir, Color.magenta);
        Debug.DrawRay(transform.position + new Vector3(0f, 0.1f, 0f), dirforw, Color.green);

        
    }
    void MoveChar()
    {
        if (grounded)
        {
            if (divergence < 80f)
            {
                Quaternion qAUX = Quaternion.LookRotation(InputMoveDir);
                Model.transform.rotation = Quaternion.Lerp(Model.transform.rotation, qAUX, 5f * Time.deltaTime);
                if (grounded) rigid.velocity = dirforw * speed;
            }
            else if (divergence > 80 && divergence < 175f)
            {
                if (speed < walkspeed) StartCoroutine("Turn90");
                else
                {
                    Quaternion qAUX = Quaternion.LookRotation(InputMoveDir);
                    Model.transform.rotation = Quaternion.Lerp(Model.transform.rotation, qAUX, 5f * Time.deltaTime);
                    if (grounded) rigid.velocity = dirforw * speed;
                }
            }
            else if (divergence > 175f)
            {
                if (speed < walkspeed) StartCoroutine("Turn180");
                else
                {
                    Quaternion qAUX = Quaternion.LookRotation(InputMoveDir);
                    Model.transform.rotation = Quaternion.Lerp(Model.transform.rotation, qAUX, 5f * Time.deltaTime);
                    if (grounded) rigid.velocity = dirforw * speed;
                }
            }
        }
        else
        {
            Quaternion qAUX = Quaternion.LookRotation(InputMoveDir);
            Model.transform.rotation = Quaternion.Lerp(Model.transform.rotation, qAUX, 2.5f * Time.deltaTime);
        }
    }

    IEnumerator Jump()
    {
        jumping = true;
        grounded = false;
        grtime = 2f;
        if (speed < 0.25)
        {
            anim.Play("jump");
            yield return new WaitForSeconds(0.125f);
        }
        else
        {
            anim.Play("runjump");
            yield return new WaitForSeconds(0.01f);
        }
        speed = speed * 0.5f;
        rigid.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        //            rigid.AddForce(Vector3.up * jumpforce + Model.transform.forward * speed * 0.0001f, ForceMode.Impulse);
        yield return new WaitForSeconds(0.3f);

        jumping = false;
    }
    IEnumerator Turn180()
    {
        anim.CrossFade("turn180", 0.1f);
        active = false;
        while (divergence > 6f)
        {
            Model.transform.Rotate(Vector3.up * -340f * Time.deltaTime);

            if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
            { speed = 1f; Aspeed = 1f; anim.SetFloat("Aspeed", 1f); }
            else { speed = 0f; Aspeed = 0f; anim.SetFloat("Aspeed", 0f); }
            yield return null;
        }
        active = true;
    }
    IEnumerator Turn90()
    {
        float spin = 1;
        if (Vector3.SignedAngle(Model.transform.forward, InputMoveDir, Vector3.up) > 0f)
        {
            anim.CrossFade("turnR90", 0.01f);
            spin = 1;
        }
        else
        {
            anim.CrossFade("turnL90", 0.01f);
            spin = -1;
        }
        active = false;
        while (divergence > 6f)
        {
            Model.transform.Rotate(Vector3.up * 237.5f * spin * Time.deltaTime);

            if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
            { speed = 1f; Aspeed = 1f; anim.SetFloat("Aspeed", 1f); }
            else { speed = 0f; Aspeed = 0f; anim.SetFloat("Aspeed", 0f); }
            yield return null;
        }
        active = true;
    }
}

