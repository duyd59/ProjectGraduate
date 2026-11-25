using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyTankCtl : MyObj
{
    [SerializeField]protected static int countDestroy = 0;
    private Transform TankDefence;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent();
    }

     protected virtual void LoadComponent()
    {
        if(this.TankDefence == null)
        {
            foreach(Transform trans in transform.parent)
            {
                if(trans.name == "TankDefense")
                {
                    this.TankDefence = trans;
                }
            }
        }
    }
     protected virtual void Update()
    {
        this.CheckDestroy();
    }

    protected virtual void CheckDestroy()
    {
        if(countDestroy == 2)
        {
            if (this.TankDefence != null)
            {
                this.TankDefence.gameObject.SetActive(false);
            }

            transform.gameObject.SetActive(false);
        }
    }
}
