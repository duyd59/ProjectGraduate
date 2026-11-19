using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.Profiling;
using Unity.VisualScripting;
using UnityEngine;

public class Boss1_Movements : MyObj
{
    [SerializeField]protected Rigidbody2D rigi;
    [SerializeField]protected CapsuleCollider2D capsule;
    [SerializeField]private Boss1_Ctl boss1_Ctl;
    [SerializeField]protected float Speed = 0.15f;
    public  override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody();
        this.LoadCollision();
        this.LoadBossCtl();
    }

    protected virtual void LoadBossCtl()
    {
        if(this.boss1_Ctl == null) this.boss1_Ctl= transform.GetComponentInParent<Boss1_Ctl>();
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
        if(this.boss1_Ctl.boss1_GManager.TargetObj == null)return;
        if(this.boss1_Ctl.Boss_ismoving == true)
        {
            transform.parent.position = Vector3.MoveTowards(transform.parent.position,this.boss1_Ctl.boss1_GManager.TargetObj.position,this.Speed);
            
            this.ControlFace(this.boss1_Ctl.boss1_GManager.TargetObj.position.x);
            if(this.boss1_Ctl._meleeAttack == true) this.Speed = 0.15f;
            else
            {
                this.Speed = 0.15f;
            }
        }
        
        
    }

    protected virtual void ControlFace(float X)
    {
        Vector3 FacePos = transform.parent.localScale;
        if((X - transform.parent.position.x) < 0 && transform.parent.localScale.x >0)
        {
            transform.parent.localScale = new Vector3(FacePos.x*-1,FacePos.y,FacePos.z);
        }
        if((X - transform.parent.position.x)> 0 && transform.parent.localScale.x <0)
        {
            transform.parent.localScale = new Vector3(FacePos.x*-1,FacePos.y,FacePos.z);;
        }

    }
}
