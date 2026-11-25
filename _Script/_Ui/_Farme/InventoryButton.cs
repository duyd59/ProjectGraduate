using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryButton : MyObj
{
    [SerializeField]protected Button InvenButton;
    [SerializeField]protected Transform Inventory;

    protected virtual void OnEnable()
    {
        this.Inventory.gameObject.SetActive(false);
    }
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent();
    }

    protected virtual void Update()
    {
        this.InventoryByClick();
        this.OnInventoryByTab();
    }
    protected virtual void LoadComponent()
    {
        if(this.InvenButton == null)
        {
            Button[] buttons = GetComponentsInChildren<Button>(true);
            foreach (Button tmp in buttons)
            {
                if (tmp.gameObject.name == "InventoryCtl")
                {
                    this.InvenButton = tmp;
                    this.InvenButton.image.sprite = Resources.Load<Sprite>("_Farme/_Ui/" + transform.name);
                    break;
                }
            }
        }

        if(this.Inventory == null)
        {
            foreach(Transform trans in transform.parent)
            {
                if(trans.name == "Inventory")
                {
                    this.Inventory = trans;
                }
            }
        }
    }

    protected virtual void InventoryByClick()
    {
        if(this.InvenButton != null)
        {
            this.InvenButton.onClick.AddListener(OnInventory);
        }
    }

    protected virtual void OnInventory()
    {
        this.Inventory.gameObject.SetActive(true);
    }

    protected virtual void OnInventoryByTab()
    {
        if(InputManger.Instance.Tab == true)
        {
            this.Inventory.gameObject.SetActive(true);
        }
    }
}
