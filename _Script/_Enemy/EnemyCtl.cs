using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtl : MyObj
{
    [SerializeField]public Vector3 EnemyPoint;
    [SerializeField]public Transform FindTarget;
    
    public virtual void SetSpawnPoint(Vector3 vtor3)
    {
        this.EnemyPoint = vtor3;
    }

    protected virtual void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            this.FindTarget = coll.gameObject.transform;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            this.FindTarget = null;
            Debug.Log("exits");
        }
    }
}
