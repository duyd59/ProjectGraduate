using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet : SkillFinalBoss1
{
    [SerializeField]protected SpawnBullet spawnBullet;
    [SerializeField]protected Transform WayBullet;

    protected virtual void OnEnable()
    {
        this.WayBullet = spawnBullet.Postank;
    }
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
        if(transform.position.x < 0)
        {
            transform.localScale = new Vector3(WayBullet.localScale.x*-1,WayBullet.localScale.y,WayBullet.localScale.z);
        }
        if(this.boss1_GManager.TargetObj == null)return;
        if(this.WayBullet.position.x >0)
        {
            transform.position+=Vector3.left;
        }
        if(this.WayBullet.position.x <0)
        {
            transform.position+=Vector3.right;
        }
        float distance = Vector3.Distance(transform.position,this.boss1_GManager.transform.position);
        if(distance >= 30)
        {
            this.spawnBullet.AddPool(transform);
            transform.gameObject.SetActive(false);
        }
    
    }
}
