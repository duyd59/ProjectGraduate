using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DameManager : MyObj
{
    [SerializeField]protected Rigidbody2D rigi;
    [SerializeField]protected Collider2D boxCollider2D;
    [SerializeField]protected Animator Ani;
    [SerializeField]protected PlayerCtl playerCtl;
    public  override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody();
        this.LoadCollision();
        this.LoadAnimator();
        this.LoadPlayerCtl();

    }
    protected virtual void LoadPlayerCtl()
    {
        if(this.playerCtl == null) this.playerCtl = transform.parent.GetComponent<PlayerCtl>();
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
        if(this.boxCollider2D == null) this.boxCollider2D = transform.parent.GetComponent<BoxCollider2D>();
    }
}
