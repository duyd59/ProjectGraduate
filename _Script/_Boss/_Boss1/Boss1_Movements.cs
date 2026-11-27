using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.Profiling;
using Unity.VisualScripting;
using UnityEngine;

public class Boss1_Movements : MyObj
{
    [SerializeField]private Boss1_Ctl boss1_Ctl;
    [SerializeField]protected float Speed = 2f;
    public  override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBossCtl();
    }

    protected virtual void LoadBossCtl()
    {
        if(this.boss1_Ctl == null) this.boss1_Ctl= transform.GetComponentInParent<Boss1_Ctl>();
    }


    protected virtual void FixedUpdate()
    {
        this.Moving();
    }

    protected virtual void Moving()
    {
        if(this.boss1_Ctl.boss1_GManager.TargetObj == null)return;
        float Distancemove = Vector3.Distance(transform.position,this.boss1_Ctl.boss1_GManager.TargetObj.position);
        if(Distancemove <= 2f)
        {
            this.Speed = 0;
        }
        if(Distancemove <= 2f)
        {
            this.Speed = 5f;
        }
        Vector3 targetPos = this.boss1_Ctl.boss1_GManager.TargetObj.position;
        Vector3 toPlayer = targetPos - transform.parent.position;
        Vector3 moveTarget = targetPos;
            float angleDeg = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg;
            float wrappedDeg = (angleDeg + 360f) % 360f;
            int dirIndex = Mathf.FloorToInt((wrappedDeg + 22.5f) / 45f) % 8;
            float snappedRad = dirIndex * 45f * Mathf.Deg2Rad;

            Vector3 snappedDir = new Vector2(Mathf.Cos(snappedRad), Mathf.Sin(snappedRad));
            moveTarget = transform.parent.position + snappedDir * (this.Speed * Time.fixedDeltaTime);

        float step = this.Speed * Time.fixedDeltaTime;
        this.transform.parent.position = Vector3.MoveTowards(transform.parent.position, moveTarget, step);
        this.ControlFace(this.boss1_Ctl.boss1_GManager.TargetObj.position.x);

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
