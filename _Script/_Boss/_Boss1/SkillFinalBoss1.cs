using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFinalBoss1 : MyObj
{
    [SerializeField]protected Rigidbody2D rigi;
    [SerializeField]protected CapsuleCollider2D capsule;
    [SerializeField]protected Boss1_GManager boss1_GManager;
    [SerializeField]protected DameSenderFinalBoss1 dameSenderFinalBoss1;
    [SerializeField]protected SpawnByBoss1 spawnByBoss1;
    public  override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody();
        this.LoadCollision();
        this.LoadBossCtl();
        this.LoadDamesender();
        this.LoadSpawnByBoss1();
    }

    protected virtual void LoadSpawnByBoss1()
    {
        if(this.spawnByBoss1 == null) this.spawnByBoss1 = transform.parent.GetComponentInParent<SpawnByBoss1>();
    }

    protected virtual void LoadDamesender()
    {
        if(this.dameSenderFinalBoss1 == null) this.dameSenderFinalBoss1 = transform.GetComponentInChildren<DameSenderFinalBoss1>();
    }

    protected virtual void LoadBossCtl()
    {
        if(this.boss1_GManager == null) this.boss1_GManager= transform.GetComponentInParent<Boss1_GManager>();
    }

    protected virtual void LoadRigidbody()
    {
        if(this.rigi == null) this.rigi = transform.GetComponent<Rigidbody2D>();
    }

    protected virtual void LoadCollision()
    {
        if(this.capsule == null) this.capsule = transform.GetComponent<CapsuleCollider2D>();
    }


    protected virtual void FixedUpdate()
    {
        this.Moving();
    }

    protected virtual void Moving()
    {
        // if(this.boss1_GManager.TargetObj == null)return;

        // if(this.spawnByBoss1.isInitialized == true)
        // {

        //     transform.position += (this.spawnByBoss1.TargetPos*Time.fixedDeltaTime*15f);
        // }
        // float distance = Vector3.Distance(transform.position,this.spawnByBoss1.transform.position);
        // if(distance >=12) this.spawnByBoss1.AddPool(transform);
    }

    protected virtual void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            this.dameSenderFinalBoss1.SendDame(coll.transform);
            this.spawnByBoss1.AddPool(transform);
            transform.gameObject.SetActive(false);
        }
    }
}
