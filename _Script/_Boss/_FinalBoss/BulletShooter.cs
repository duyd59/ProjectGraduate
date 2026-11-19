using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletShooter : SkillFinalBoss1
{
    [SerializeField]protected SpawnBullet spawnBullet;
    [SerializeField]protected Vector3 DirectionPos;
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent();
    }

    protected virtual void LoadComponent()
    {
        if(this.spawnBullet == null) this.spawnBullet = transform.parent.GetComponentInParent<SpawnBullet>();
    }
    protected override void Moving()
    {
        base.Moving();
        if(this.boss1_GManager.TargetObj == null)return;
        if(transform.position == this.spawnBullet.BulletPos1)
        {
            Debug.Log("11");
            DirectionPos = this.boss1_GManager.TargetObj.position - this.spawnBullet.BulletPos1;
            
        }
        if(transform.position == this.spawnBullet.BulletPos2)
        {
            DirectionPos = this.boss1_GManager.TargetObj.position -this.spawnBullet.BulletPos2;
            
        }
        transform.position += (this.DirectionPos*Time.fixedDeltaTime*2f);
        
        float distance = Vector3.Distance(transform.position,this.DirectionPos);
        if(distance >= 50)
        {
            this.spawnByBoss1.AddPool(transform);
            transform.gameObject.SetActive(false);
        }
    }
}
