using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyByBoss2DameSender : MyObj
{
    public virtual void SendDame(Transform trans)
    {
        DameReceiver dameReceiver = trans.GetComponentInChildren<DameReceiver>();
        if(dameReceiver != null)
        {
            dameReceiver.Deduct(1);
        }
    }
}
