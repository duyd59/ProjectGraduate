using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIVCtl : InventoryView
{
    [SerializeField]protected Button ButtonWold;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadButtonW();
    }

    protected override void Update()
    {
        base.Update();
        this.Makefarm();
    }
    protected virtual void LoadButtonW()
    {
        Button[] buttons = GetComponentsInChildren<Button>(true);
        foreach (Button tmp in buttons)
        {
            if (tmp.gameObject.name == "ButtonWold")
            {
                this.ButtonWold = tmp;
                break;
            }
        }
    }

    protected virtual void Makefarm()
    {
        if(this.ButtonWold != null)
        {
            this.ButtonWold.onClick.AddListener(ButtonByTable);
        }       
    }

    protected virtual void ButtonByTable()
    {
        string resourceName = transform.parent.name;
        int Count = 0;;
        switch (resourceName)
        {
            case "Meat":
                Count = this.farme_ScipTble.Meat;
                this.player_ScripTble.Meat += Count;
                this.farme_ScipTble.Meat = 0;
                break;
            case "Fur":
                if(this.farme_ScipTble.Fur >= 50 && this.player_ScripTble.Coat == 0)
                {
                    this.player_ScripTble.Coat += 1;
                    this.farme_ScipTble.Fur -= 50;
                }
                break;
            case "Flower":
                Count = (this.farme_ScipTble.Flower );
                this.player_ScripTble.HpHeal += Count/2;
                this.farme_ScipTble.Flower -= Count;
                break;
        }
        Debug.Log("dsfsdg");
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
