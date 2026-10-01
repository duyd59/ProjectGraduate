using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDameSender : DameManager
{
    public virtual void SendDame(Transform Enemy)
    {
        DameReceiver ObjDame = Enemy.GetComponentInChildren<DameReceiver>();
        if(ObjDame != null)
        {
            ObjDame.Deduct(this.playerCtl.player_ScripTble.Dmae);
        }
    }
}
