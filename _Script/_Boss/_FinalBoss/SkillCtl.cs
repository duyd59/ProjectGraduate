using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCtl : MyObj
{
    [SerializeField]protected SpawnByBoss1 spawnByBoss1;
    public  override void LoadComponents()
    {
        base.LoadComponents();
        
        this.LoadSpawnByBoss1();
    }

    protected virtual void LoadSpawnByBoss1()
    {
        if(this.spawnByBoss1 == null) this.spawnByBoss1 = transform.parent.GetComponentInParent<SpawnByBoss1>();
    }
    public virtual void DestroyObj()
    {
        this.spawnByBoss1.AddPool(transform);
    }
}
