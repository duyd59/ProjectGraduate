using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2EDameREceiver : MyObj
{
    [SerializeField]public float CurrentHp;
    [SerializeField]protected bool Isdead;
    [SerializeField]public Enemy0 enemy0;
    [SerializeField]protected SpawnByBoss2 spawnByBoss2;

    protected virtual void OnEnable()
    {
        this.LoadCurrentHp();
    }
    protected virtual void LoadEnemy()
    {
        if(this.enemy0 == null)
        {
            this.enemy0 = transform.parent.GetComponent<Enemy0>();
        }
    }

    protected virtual void LoadSpawner()
    {
        if(this.spawnByBoss2 == null)
        {
            this.spawnByBoss2 = transform.parent.GetComponentInParent<SpawnByBoss2>();
        }
    }

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawner();
        this.LoadEnemy();
    }

    protected virtual void LoadCurrentHp()
    {
        this.CurrentHp = this.enemy0.boss_ScripTble.CurrentHp;   
    }

   
    protected virtual void Update()
    {
        this.CheckStatus();
    }

    public virtual void AddHP(float Hp)
    {
        
    }

    public virtual void DeductHP(float dame)
    {
        float Dame = dame - this.enemy0.boss_ScripTble.Amor;
        if(Dame <=0) return;
        this.CurrentHp-=Dame;
        if(this.CurrentHp <= 0) this.Isdead = true;
        this.CheckStatus();
    }

    protected virtual void CheckStatus()
    {
        if(this.Isdead == true)
        {
            transform.parent.gameObject.SetActive(false);
            this.spawnByBoss2.AddPool(transform.parent);
            this.spawnByBoss2.CountCurrentObj--;
            this.Isdead= false;

        }
    }
}
