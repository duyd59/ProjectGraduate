using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCtl : MyObj
{
    [SerializeField]protected FarmeManager farmeManager;
    [SerializeField]protected Transform ViewPOs;
    [SerializeField]protected List<Transform> InvenList = new List<Transform>();
    [SerializeField]protected List<Transform> CurrentInven = new List<Transform>();
    [SerializeField]protected Button InvenB;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent();
    }

    protected virtual void OnEnable()
    {
        this.ShowView();
    }

    protected virtual void Update()
    {
        this.ExitInventory();
    }

    protected virtual void LoadComponent()
    {
        if(this.farmeManager == null)
        {
            this.farmeManager = transform.parent.GetComponentInParent<FarmeManager>();
        }
        if(this.ViewPOs == null)
        {
            foreach(Transform trans in transform)
            {
                if(trans.name == "Viewport")
                {
                    foreach(Transform transf in trans)
                    {
                        if(transf.name == "Content")
                        {
                            this.ViewPOs = transf;
                        }
                    }
                }
            }
        }

        if(this.InvenList.Count <= 0)
        {
            foreach(Transform trans in transform.parent)
            {
                if(trans.name == "Dictionary")
                {
                    foreach(Transform transf in trans)
                    {
                        this.InvenList.Add(transf);
                    }
                }
            }
        }

        if(this.InvenB == null)
        {
            Button[] buttons = GetComponentsInChildren<Button>(true);
            foreach (Button tmp in buttons)
            {
                if (tmp.gameObject.name == "InventoryB")
                {
                    this.InvenB = tmp;
                    break;
                }
            }
        }
    }

    protected virtual void ExitInventory()
    {
        if(this.InvenB != null)
        {
            this.InvenB.onClick.AddListener(ExitI);
        }
    }

    protected virtual void ExitI()
    {
        transform.gameObject.SetActive(false);
        InputManger.Instance.SetTab();
    }

    protected virtual void ShowView()
    {
        
        if(this.CurrentInven.Count <= 0){
            if(this.farmeManager.farme_ScipTble.Meat > 0)
            {
                Transform NewObj = Instantiate(this.InvenList[0],this.ViewPOs,false);
                NewObj.name = "Meat";
                NewObj.SetParent(this.ViewPOs,false);
                this.CurrentInven.Add(NewObj);
                NewObj.gameObject.SetActive(true);
            }
            if(this.farmeManager.farme_ScipTble.Fur > 0)
            {
                Transform NewObj = Instantiate(this.InvenList[1],this.ViewPOs,false);
                NewObj.name = "Fur";
                this.CurrentInven.Add(NewObj);
                NewObj.SetParent(this.ViewPOs,false);
                //NewObj.gameObject.SetActive(true);
                InventoryView inventoryView = NewObj.GetComponent<InventoryView>();
                inventoryView.textMeshView.text = $"{this.farmeManager.farme_ScipTble.Fur}";
            }
            if(this.farmeManager.farme_ScipTble.Flower > 0)
            {
                Transform NewObj = Instantiate(this.InvenList[2],this.ViewPOs,false);
                NewObj.name = "Flower";
                this.CurrentInven.Add(NewObj);
                NewObj.SetParent(this.ViewPOs,false);
                NewObj.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach(Transform trans in this.CurrentInven)
            {
                trans.gameObject.SetActive(true);
            }
        }
    }
    
}
