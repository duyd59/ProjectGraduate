using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDameReceiver : DameManager
{
    [SerializeField]protected bool Isdead;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadHp();
    }

    protected virtual void Update()
    {
        this.CheckStatus();
    }

    protected virtual void LoadHp()
    {
        this.playerCtl.player_ScripTble.ResetInfo();
    }

    public virtual void Deduct(float Hp)
    {
        if(this.playerCtl.player_ScripTble.CurrentHp <= 0) this.Isdead = true;
        else
        {
            this.playerCtl.player_ScripTble.CurrentHp-=Hp;
        }
    }

    public virtual void AddHp(float Hp)
    {
        
    }

    protected virtual void CheckStatus()
    {
        if(this.Isdead == true)
        {
            Debug.Log("you dead");
            this.Isdead= false;
        }
    }
}
