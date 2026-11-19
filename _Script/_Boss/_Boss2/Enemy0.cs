using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Enemy0 : EnemyByBoss2Movements
{
    [SerializeField]protected bool IsAttack;
    protected virtual void OnEnable()
    {
        this.boss_ScripTble.ResetInfo();
    }
    protected override void Moving()
    {
        base.Moving();
        
        float distance = Vector3.Distance(transform.position,this.boss1_GManager.TargetObj.position);
        if (distance <= 0.5f)
        {
            this.IsAttack = true;
            this.ani.SetBool("_Attack",this.IsAttack);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position,this.boss1_GManager.TargetObj.position,0.1f);
            this.IsAttack = false;
        }

    }
     protected virtual void Update()
    {
        this.ControlFace(this.boss1_GManager.TargetObj.position.x);
        
    }
    protected virtual void ControlFace(float X)
    {
        Vector3 FacePos = transform.localScale;
        if((X - transform.position.x) < 0 && transform.localScale.x >0)
        {
            transform.localScale = new Vector3(FacePos.x*-1,FacePos.y,FacePos.z);
        }
        if((X - transform.position.x)> 0 && transform.localScale.x <0)
        {
            transform.localScale = new Vector3(FacePos.x*-1,FacePos.y,FacePos.z);;
        }

    }
    protected virtual bool IsDame()
    {
        float facingDir = Mathf.Sign(transform.localScale.x);
        Vector3 dir = (transform.right*0.5f);
        if(transform.localScale.x == -1)
        {
            dir*=-1;
        }
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(this.boxCollider2D.bounds.center + transform.right* range *facingDir - dir,
        new Vector3(this.boxCollider2D.bounds.size.x,this.boxCollider2D.bounds.size.y,this.boxCollider2D.bounds.size.z),
        0,Vector2.left,0,this.LayerMask);

        if(raycastHit2D.collider != null)
        {
            this.ObjDame = raycastHit2D.transform;
            return true;
        }
        return false;
    }

    protected virtual void OnDrawGizmos()
    {
        float facingDir = Mathf.Sign(transform.localScale.x);
        Vector3 dir = (transform.right*0.5f);
        if(transform.localScale.x == -1)
        {
            dir*=-1;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.boxCollider2D.bounds.center  + (transform.right* range *facingDir - dir),
        new Vector3(this.boxCollider2D.bounds.size.x,this.boxCollider2D.bounds.size.y,this.boxCollider2D.bounds.size.z));
    }

    public virtual void EnemyAttack()
    {
        if (this.IsDame())
        {
            this.enemyByBoss2DameSender.SendDame(this.ObjDame);
        }
    }
}
