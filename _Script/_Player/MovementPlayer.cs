using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Movement : MyObj
{
    [SerializeField]protected Rigidbody2D rigi;
    [SerializeField]protected CapsuleCollider2D capsule;
    [SerializeField]protected Animator Ani;
    [SerializeField]protected float Speed = 5f;
    public  override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody();
        this.LoadCollision();
        this.LoadAnimator();
    }

    protected virtual void LoadAnimator()
    {
        if(this.Ani == null) this.Ani = transform.parent.GetComponent<Animator>();
    }

    protected virtual void LoadRigidbody()
    {
        if(this.rigi == null) this.rigi = transform.parent.GetComponent<Rigidbody2D>();
    }

    protected virtual void LoadCollision()
    {
        if(this.capsule == null) this.capsule = transform.parent.GetComponent<CapsuleCollider2D>();
    }
    protected virtual void FixedUpdate()
    {
        this.Moving();
    }

    protected virtual void Moving()
    {
        Vector3 MoVecter = new Vector3(InputManger.Instance.Horizontal,
                                        InputManger.Instance.Vertical,
                                        0f);
        this.rigi.velocity = MoVecter*this.Speed;
        this.ControlFace();
        this.ControlAni();

    }

    protected virtual void ControlFace()
    {
        Vector3 FacePos = transform.parent.localScale;
        if(InputManger.Instance.Horizontal < 0 && transform.parent.localScale.x >0)
        {
            transform.parent.localScale = new Vector3(FacePos.x*-1,FacePos.y,FacePos.z);
        }
        if(InputManger.Instance.Horizontal > 0&& transform.parent.localScale.x <0)
        {
            transform.parent.localScale = new Vector3(FacePos.x*-1,FacePos.y,FacePos.z);;
        }
    }

    protected virtual void ControlAni()
    {
        this.Ani.SetFloat("_Hozirontal",Mathf.Abs(InputManger.Instance.Horizontal));
        this.Ani.SetFloat("_Vertical",Mathf.Abs(InputManger.Instance.Vertical));
        this.Ani.SetBool("Attack1",InputManger.Instance.LeftMouse);
        this.Ani.SetBool("Attack2",InputManger.Instance.RightMouse);
    }
}
