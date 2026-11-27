using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableButton : MyObj
{
    [SerializeField]protected UnityEngine.UI.Button TableB;
    [SerializeField]protected Transform TableScroll;

    protected virtual void OnEnable()
    {
        this.TableScroll.gameObject.SetActive(false);
    }
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent();
    }

    protected virtual void Update()
    {
        this.TableByClick();
        this.TAbleByTab();
    }
    protected virtual void LoadComponent()
    {
        if(this.TableB == null)
        {
            Button[] buttons = GetComponentsInChildren<Button>(true);
            foreach (Button tmp in buttons)
            {
                if (tmp.gameObject.name == "InventoryCtl")
                {
                    this.TableB = tmp;
                    this.TableB.image.sprite = Resources.Load<Sprite>("_Farme/_Ui/" + transform.name);
                    break;
                }
            }
        }

        if(this.TableScroll == null)
        {
            foreach(Transform trans in transform.parent)
            {
                if(trans.name == "TableScroll")
                {
                    this.TableScroll = trans;
                }
            }
        }
    }

    protected virtual void TableByClick()
    {
        if(this.TableB != null)
        {
            this.TableB.onClick.AddListener(OnTable);
        }
    }

    protected virtual void OnTable()
    {
        this.TableScroll.gameObject.SetActive(true);
    }

    protected virtual void TAbleByTab()
    {
        if(InputManger.Instance.Tab == true)
        {
            this.TableScroll.gameObject.SetActive(true);
        }
    }
}
