using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class FarmeManager : MyObj
{
    [SerializeField]protected Farme_ScipTble farme_ScipTble;
    [SerializeField]public float CurrentXp;
    [SerializeField]protected int FarmeLv;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadScripTble();
    }

    private void Update()
    {
        this.UpgradeLv();
    }

    protected virtual void UpgradeLv()
    {
        
        if(this.CurrentXp >= this.farme_ScipTble.MaxXp)
        {
            this.farme_ScipTble.SetMaxXp();
            this.CurrentXp -= this.farme_ScipTble.CurrentXP;
            this.FarmeLv = this.farme_ScipTble.CurrentLv;
            if(this.FarmeLv>=this.farme_ScipTble.MaxLv) return;
            
        }
    }
    protected virtual void LoadScripTble()
    {
        if(this.farme_ScipTble == null)
        {
            string Obj = transform.name;
            this.farme_ScipTble = Resources.Load<Farme_ScipTble>("_Farme/" + Obj);
        }
        else
        {
            this.farme_ScipTble.SetMaxXp();
            this.CurrentXp = this.farme_ScipTble.CurrentXP;
            this.FarmeLv = this.farme_ScipTble.CurrentLv;
        }
    }
}
