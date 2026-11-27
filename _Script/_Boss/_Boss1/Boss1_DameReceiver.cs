using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss1_DameReceiver : MyObj
{
    [SerializeField]protected bool Isdead;
    [SerializeField]protected float currentHp;
    [SerializeField]protected Boss1_Ctl boss1_Ctl;
    // public float CurrentHp => currentHp;

    // [SerializeField]protected float hpMax;
    // public float HpMax => hpMax;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBossCtl();
        this.LoadHp();
    }

    protected virtual void LoadBossCtl()
    {
        if(this.boss1_Ctl == null) this.boss1_Ctl = transform.parent.GetComponent<Boss1_Ctl>();
    }

    protected virtual void Update()
    {
        this.CheckStatus();
    }

    protected virtual void LoadHp()
    {
        this.boss1_Ctl.Boss_ScripTble.ResetInfo();
        this.currentHp = this.boss1_Ctl.Boss_ScripTble.CurrentHp;
    }

    public virtual void Deduct(float Hp)
    {
        if(this.currentHp <= 0) this.Isdead = true;
        else
        {
            this.currentHp-=Hp;
        }
    }

    public virtual void AddHp(float Hp)
    {
        
    }

    protected virtual void CheckStatus()
    {
        if(this.Isdead == true)
        {
            this.Isdead= false;
            transform.parent.gameObject.SetActive(false);
        }
    }

    protected virtual void OnDisable()
    {
        PlayerCtl playerCtl = this.boss1_Ctl.boss1_GManager.TargetObj.GetComponent<PlayerCtl>();
        if(playerCtl != null)
        {
            if(SceneManager.GetActiveScene().name == "Boss_1")
            {
                 playerCtl.player_ScripTble.boss1 = true;
            }
            if(SceneManager.GetActiveScene().name == "Boss_2")
            {
                 playerCtl.player_ScripTble.boss2 = true;
            }
        }
    }
}
