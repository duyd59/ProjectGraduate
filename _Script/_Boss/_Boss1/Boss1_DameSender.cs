using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_DameSender : MyObj
{
    [SerializeField]protected Boss1_Ctl boss1_Ctl;
    [SerializeField]protected bool IsAttack;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBoss1Ctl();
    }

    protected virtual void LoadBoss1Ctl()
    {
        if(this.boss1_Ctl == null) this.boss1_Ctl = transform.parent.GetComponent<Boss1_Ctl>();
    }

    public virtual void SendDame(Transform trans)
    {
        DameReceiver dameReceiver = trans.GetComponentInChildren<DameReceiver>();
        if(dameReceiver != null)
        {
            if (this.boss1_Ctl._Issender)
            {
                dameReceiver.Deduct(boss1_Ctl.Boss_ScripTble.Dame);
                this.boss1_Ctl._Issender = false;
            }
            
        }
    }
}
