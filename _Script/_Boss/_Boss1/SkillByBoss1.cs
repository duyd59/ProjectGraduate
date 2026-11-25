using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillByBoss1 : SkillFinalBoss1
{
    protected override void Moving()
    {
        base.Moving();
        if(this.boss1_GManager.TargetObj == null)return;
        
        transform.position = Vector3.MoveTowards(transform.position,this.spawnByBoss1.TarGetSkill,0.5f);
        float distance = Vector3.Distance(transform.position,this.spawnByBoss1.TarGetSkill);
        if (distance <= 0.5f)
        {
            this.spawnByBoss1.GetBySpawn(transform);
            this.spawnByBoss1.AddPool(transform);
            transform.gameObject.SetActive(false);
        }
    }
}
