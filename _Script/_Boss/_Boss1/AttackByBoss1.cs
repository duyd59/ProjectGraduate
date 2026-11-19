using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackByBoss1 : SkillFinalBoss1
{
    protected override void Moving()
    {
        base.Moving();
        if(this.boss1_GManager.TargetObj == null)return;
        transform.position += (this.spawnByBoss1.TargetPos*Time.fixedDeltaTime*40f);
        
        float distance = Vector3.Distance(transform.position,this.spawnByBoss1.transform.position);
        if(distance >= 50)
        {
            this.spawnByBoss1.AddPool(transform);
            transform.gameObject.SetActive(false);
        }
    }
}
