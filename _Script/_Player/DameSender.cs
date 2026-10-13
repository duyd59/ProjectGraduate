using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class DameSender : DameManager
{
    // [SerializeField]protected DameReceiver dameReceiver;

    // public override void LoadComponents()
    // {
    //     base.LoadComponents();
    //     this.LoadDameReceiver();
    // }

    // protected virtual void LoadDameReceiver()
    // {
    //     if(this.dameReceiver == null)
    //     {
    //         GameObject Obj = GameObject.Find("Player");
    //         foreach(Transform trans in Obj.transform)
    //         {
    //             if(trans.name == "DameReceiver")
    //             {
    //                 this.dameReceiver = trans.GetComponent<DameReceiver>();
    //             }
    //         }
    //     }
    // }
    public virtual void SendDame(Transform Enemy)
    {
        EnemyDameReceiver ObjDame = Enemy.GetComponentInChildren<EnemyDameReceiver>();
        if(ObjDame != null)
        {
            ObjDame.DeductHP();
        }
    }
}
