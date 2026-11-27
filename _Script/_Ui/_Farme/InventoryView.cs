using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryView : MyObj
{
    [SerializeField]private UnityEngine.UI.Image ImageView;
    [SerializeField]public TextMeshProUGUI textMeshView;
    [SerializeField]protected FarmeManager farmeManager;
    [SerializeField]protected Player_ScripTble player_ScripTble;
    [SerializeField]protected Farme_ScipTble farme_ScipTble;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent();
    }
    protected virtual void LoadComponent()
    {
        if(this.farmeManager == null) this.farmeManager = transform.parent.GetComponentInParent<FarmeManager>();
        if(this.player_ScripTble == null)
        {
            string LoadPath = "_Player/Player";
            this.player_ScripTble = Resources.Load<Player_ScripTble>(LoadPath);
        }
        if(this.farme_ScipTble == null)
        {
            string Obj = "FarmeManager";
            this.farme_ScipTble = Resources.Load<Farme_ScipTble>("_Farme/" + Obj);
        }
        if(this.ImageView == null)
        {
            UnityEngine.UI.Image[] images = GetComponentsInChildren<UnityEngine.UI.Image>(true);
            foreach (UnityEngine.UI.Image img in images)
            {
                if (img.gameObject.name == "Image")
                {
                    this.ImageView = img;
                    this.ImageView.sprite = Resources.Load<Sprite>("_Farme/_Ui/" + transform.name);
                    break;
                }
            }
        }

        if (this.textMeshView == null)
        {
            TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>(true);
            foreach (TextMeshProUGUI tmp in texts)
            {
                if (tmp.gameObject.name == "CountInventory")
                {
                    this.textMeshView = tmp;
                    break;
                }
            }
        }
    }

    protected virtual void Update()
    {
        UpdateUI();
    }


    private void UpdateUI()
    {
        string resourceName = transform.name;
        int amount = 0;

        switch (resourceName)
        {
            case "Meat":
                amount = this.farmeManager.farme_ScipTble.Meat;
                break;
            case "Fur":
                amount = this.farmeManager.farme_ScipTble.Fur;
                break;
            case "Flower":
                amount = this.farmeManager.farme_ScipTble.Flower;
                break;
            case "MeatC":
                amount = this.farmeManager.farme_ScipTble.Meat;
                break;
            case "HPHeal":
                amount = (this.farmeManager.farme_ScipTble.Flower / 2);
                break;
            case "Coat":
                if(this.player_ScripTble.Coat == 0)
                {
                    amount = 1;
                }
                else
                {
                    amount = 0;
                }
                break;
        }

        if (textMeshView != null)
        {
            textMeshView.text = $"{amount}";
        }
    }
}
