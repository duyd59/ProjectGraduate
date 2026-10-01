using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCtl : MyObj
{
    [SerializeField]protected Spawner spawner;

    public override void LoadComponents()
    {
        this.LoadSpawner();
    }

    protected virtual void LoadSpawner()
    {
        if(this.spawner == null)
        {
            GameObject Obj = GameObject.FindGameObjectWithTag("TutorialManager");
            this.spawner = Obj.GetComponent<Spawner>();
        }
    }
}
