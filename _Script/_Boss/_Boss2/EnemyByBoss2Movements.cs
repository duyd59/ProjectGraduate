using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyByBoss2Movements : MyObj
{
    [SerializeField]protected Rigidbody2D rigi;
    [SerializeField]protected Transform ObjDame;
    [SerializeField]protected float range = 0.5f;
    [SerializeField]protected CapsuleCollider2D capsule;
    [SerializeField]protected SpawnByBoss2 spawnByBoss2;
    [SerializeField]protected EnemyByBoss2DameSender enemyByBoss2DameSender;
    [SerializeField]protected Boss2EDameREceiver boss2EDameREceiver;
    [SerializeField]protected Boss1_GManager boss1_GManager;
    [SerializeField]protected Animator ani;
    [SerializeField]public Boss_ScripTble boss_ScripTble;
    [SerializeField]protected LayerMask LayerMask;
    [SerializeField]protected BoxCollider2D boxCollider2D;



    public  override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody();
        this.LoadCollision();
        this.LoadDamesender();
        this.LoadSpawnByBoss1();
        this.LoadAnimator();
        this.LoadScripTble();
    }

    protected virtual void LoadScripTble()
    {
        if(this.boss_ScripTble == null)
        {
            this.boss_ScripTble = Resources.Load<Boss_ScripTble>("_Boss/" +transform.name);
        }
    }

    protected virtual void LoadAnimator()
    {
        if(this.ani ==null) this.ani = transform.GetComponent<Animator>();
        this.LayerMask = LayerMask.GetMask("Player");
        if(this.boxCollider2D == null) this.boxCollider2D = transform.GetComponent<BoxCollider2D>();
    }

    protected virtual void LoadSpawnByBoss1()
    {
        if(this.spawnByBoss2 == null) this.spawnByBoss2 = transform.parent.GetComponentInParent<SpawnByBoss2>();
        if(this.boss1_GManager == null)
        {
            this.boss1_GManager = GameObject.FindWithTag("FinalBoss").transform.GetComponent<Boss1_GManager>();
            Debug.Log(GameObject.FindWithTag("FinalBoss"));
        }
    }

    protected virtual void LoadDamesender()
    {
        if(this.enemyByBoss2DameSender == null) this.enemyByBoss2DameSender = transform.GetComponentInChildren<EnemyByBoss2DameSender>();
        if(this.boss2EDameREceiver == null) this.boss2EDameREceiver = transform.GetComponentInChildren<Boss2EDameREceiver>();
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
            this.enemyByBoss2DameSender.SendDame(coll.transform);
        }
    }
}
