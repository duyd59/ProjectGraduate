using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnByBoss1 : Spawner
{
    [SerializeField]protected Boss1_GManager boss1_GManager;
    [SerializeField]protected Vector3 AttackPos = new Vector3(0f,8f,0f);
    [SerializeField]public Vector3 TargetPos;
    [SerializeField]public Vector3 TarGetSkill;
    [SerializeField]protected float CurrentTimeAttack;
    [SerializeField]protected float CurrentTimeSkill;


    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBossGmanager();
    }

    protected virtual void LoadBossGmanager()
    {
        if(this.boss1_GManager == null)
        {
            this.boss1_GManager = transform.GetComponent<Boss1_GManager>();
        }
    }

    protected virtual void FixedUpdate()
    {
        this.FinalBossAttacking();
    }

    protected virtual void FinalBossAttacking()
    {
        if (this.CurrentTimeAttack <= this.boss1_GManager.boss_ScripTble._TimeAttack) this.CurrentTimeAttack+=Time.fixedDeltaTime; 
        if (this.CurrentTimeAttack <= this.boss1_GManager.boss_ScripTble._TimeAttack) this.CurrentTimeSkill+=Time.fixedDeltaTime;

        if(this.CurrentTimeAttack >=this.boss1_GManager.boss_ScripTble._TimeAttack)
        {
            this.Spawning(this.ListObj[0],AttackPos,transform.rotation);
            this.Init(this.boss1_GManager.TargetObj.position);
            this.CurrentTimeAttack = 0;
        }
        if(this.CurrentTimeSkill >=this.boss1_GManager.boss_ScripTble._TimeSkill)
        {
            this.Spawning(this.ListObj[1],AttackPos,transform.rotation);
            this.CurrentTimeSkill = 0;
            this.TarGetSkill = this.boss1_GManager.TargetObj.position;
        }
    }
    public void Init(Vector3 target)
    {
        this.TargetPos = (target - this.AttackPos).normalized;
    }
}
