using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class FarmeManager : MyObj
{
    [SerializeField]public Farme_ScipTble farme_ScipTble;
    [SerializeField]public LocalsaveManager LocalsaveManager;
    [SerializeField]public float CurrentXp;
    [SerializeField]protected int FarmeLv;

    public override void Awake()
    {
        base.Awake();
        this.LoadJson();
    }

    protected virtual void LoadJson()
    {
        if(this.LocalsaveManager != null)
        {
            FarmeSaveDate date = this.LocalsaveManager.LoadFromJson();

            this.farme_ScipTble.Meat = date.Meat;
            this.farme_ScipTble.Fur = date.Fur;
            this.farme_ScipTble.Flower = date.Flower;
            this.farme_ScipTble.CurrentXP = date.CurretXp;
            this.farme_ScipTble.FarmeLv = date.FarmeLevel;
        }
    }
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
        
        if(this.farme_ScipTble.CurrentXP >= this.farme_ScipTble.MaxXp)
        {
            this.farme_ScipTble.SetMaxXp();
            if(this.farme_ScipTble.FarmeLv>=this.farme_ScipTble.MaxLv) return;
            
        }
    }
    protected virtual void LoadScripTble()
    {
        if(this.farme_ScipTble == null)
        {
            string Obj = transform.name;
            this.farme_ScipTble = Resources.Load<Farme_ScipTble>("_Farme/" + Obj);
        }
        if(this.LocalsaveManager == null) this.LocalsaveManager = transform.GetComponent<LocalsaveManager>();
    }
}
