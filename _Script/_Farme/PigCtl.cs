using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PigCtl : MyObj
{
    [SerializeField]public bool IsHarvest;
    [SerializeField]public float CurrentTime;
    [SerializeField]public Animal_ScripTble animal_ScripTble;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadScripTble();
    }

    protected virtual void LoadScripTble()
    {
        if(this.animal_ScripTble == null)
        {
            string Obj = transform.name;
            this.animal_ScripTble = Resources.Load<Animal_ScripTble>("_Farme/_Animal/" + Obj);
        }
    }

    protected virtual void FixedUpdate()
    {
        this.CheckStatusHarvest();
    }

    protected virtual void CheckStatusHarvest()
    {
        this.CurrentTime+=Time.fixedDeltaTime;
        if(this.CurrentTime >= this.animal_ScripTble.TimeMax)
        {
            this.IsHarvest = true;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            SetTime();
        }
    }

    public virtual void SetTime()
    {
        Debug.Log("fdsjfkjsdb");
        this.CurrentTime = 0f;
        this.IsHarvest = false;
    }
}
