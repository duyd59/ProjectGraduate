using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public  class HarvestCtl : MyObj
{
    [SerializeField]protected SpawnAnimal spawnAnimal;
    [SerializeField]protected FarmeManager farmeManager;
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawnAnimal();
        this.LoadFarmeManager();
    }

    protected virtual void LoadFarmeManager()
    {
        if(this.farmeManager == null)
        {
            this.farmeManager = transform.parent.GetComponentInParent<FarmeManager>();
        }
    }

    protected virtual void LoadSpawnAnimal()
    {
        if(this.spawnAnimal == null)
        {
            this.spawnAnimal = transform.parent.GetComponentInParent<SpawnAnimal>();
        }
    }
    //protected abstract string AnimalName();
    protected virtual void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            foreach(Transform trans in this.spawnAnimal.Holder)
            {
                if(trans.name == transform.name)
                {
                    PigCtl Harvest = trans.GetComponent<PigCtl>();
                    if(Harvest.IsHarvest == true)
                    {
                        this.farmeManager.farme_ScipTble.CurrentXP+=Harvest.animal_ScripTble.Xp;
                        this.farmeManager.farme_ScipTble.Meat +=Harvest.animal_ScripTble.Meat;
                        this.farmeManager.farme_ScipTble.Fur +=Harvest.animal_ScripTble.Fur;
                        Harvest.SetTime();
                    }
                }
            }

            FarmeSaveDate date = new FarmeSaveDate
            {
                Meat = this.farmeManager.farme_ScipTble.Meat,
                Fur = this.farmeManager.farme_ScipTble.Fur,
                Flower = this.farmeManager.farme_ScipTble.Flower,
                FarmeLevel = this.farmeManager.farme_ScipTble.FarmeLv,
                CurretXp = this.farmeManager.farme_ScipTble.CurrentXP
            };

            this.farmeManager.LocalsaveManager.SaveToJson(date);
        }
    }
}
