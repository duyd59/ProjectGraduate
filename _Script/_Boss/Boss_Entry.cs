using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Entry : MyObj
{
    [SerializeField]protected Transform IsSee;
    [SerializeField]protected Player_ScripTble player_ScripTble;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadIsSee();
    }

    protected virtual void Update()
    {
        this.CheckKeyBoss();
    }

    protected virtual void CheckKeyBoss(){
        if(this.player_ScripTble.boss1 == true && this.player_ScripTble.boss2)
        {
            this.player_ScripTble.BossKey = true;
        }
    }

    protected virtual void LoadIsSee()
    {
        if(this.IsSee == null)
        {
            foreach(Transform trans in transform)
            {
                this.IsSee = trans;
            }
        }
        if(this.player_ScripTble == null)
        {
            string LoadPath = "_Player/Player";
            this.player_ScripTble = Resources.Load<Player_ScripTble>(LoadPath);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            if(this.player_ScripTble.BossKey == true)
            {
                this.IsSee.gameObject.SetActive(false);
            }
       }
    }
}
