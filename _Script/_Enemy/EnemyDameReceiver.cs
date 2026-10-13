using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class EnemyDameReceiver : MyObj
{
    [SerializeField]public float CurrentHp;
    [SerializeField]protected bool Isdead;
    [SerializeField]public SpawnTutorial spawner;

    protected virtual void OnEnable()
    {
        this.LoadCurrentHp();
    }
    protected virtual void LoadSpawner()
    {
        if(this.spawner == null)
        {
            GameObject Obj = GameObject.FindGameObjectWithTag("TutorialManager");
            this.spawner = Obj.GetComponent<SpawnTutorial>();
        }
    }

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawner();
        //this.LoadCurrentHp();
    }

    protected virtual void LoadCurrentHp()
    {
        foreach(Enemy_ScripTble enemy_ScripTble in this.spawner.listEnemy_ScripTble)
        {
            if(enemy_ScripTble.name == transform.parent.name)
            {
                this.CurrentHp = enemy_ScripTble.CurrentHp;
            }
        }
    }

   
    protected virtual void Update()
    {
        this.CheckStatus();
    }

    public virtual void AddHP(float Hp)
    {
        
    }

    public virtual void DeductHP()
    {
        this.CurrentHp-=1;
        if(this.CurrentHp <= 0) this.Isdead = true;
        this.CheckStatus();
    }

    protected virtual void CheckStatus()
    {
        if(this.Isdead == true)
        {
            transform.parent.gameObject.SetActive(false);
            this.spawner.AddPool(transform.parent);
            this.spawner.CountCurrentObj--;
            this.Isdead= false;

        }
    }
}
