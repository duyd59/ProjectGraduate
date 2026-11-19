using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_GManager : MyObj
{
    [SerializeField]public Transform TargetObj;
    [SerializeField]protected BoxCollider2D boxCollider2D;
    [SerializeField]public Boss_ScripTble boss_ScripTble;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider();
        this.LoadScripTble();
    }

    protected virtual void LoadScripTble()
    {
        if(this.boss_ScripTble == null)
        {
            this.boss_ScripTble = Resources.Load<Boss_ScripTble>("_Boss/" + transform.name);
        }
    }
    protected virtual void LoadCollider()
    {
        if(this.boxCollider2D == null) this.boxCollider2D = transform.GetComponent<BoxCollider2D>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            this.TargetObj = coll.transform;
            this.boxCollider2D.enabled = false;
        }
    }

    
}

