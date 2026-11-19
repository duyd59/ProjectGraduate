using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Entry : MyObj
{
    [SerializeField]protected Transform IsSee;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadIsSee();
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
    }

    protected virtual void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            PlayerCtl playerCtl = coll.gameObject.GetComponent<PlayerCtl>();
            if(playerCtl.BossKey == true)
            {
                this.IsSee.gameObject.SetActive(false);
            }
       }
    }
}
