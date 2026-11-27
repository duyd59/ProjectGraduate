using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTableB : MyObj
{
    [SerializeField]protected Button EndButton;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent();
    }

    protected virtual void Update()
    {
        this.ButtonT();
    }
    protected virtual void LoadComponent()
    {
        Button[] buttons = GetComponentsInChildren<Button>(true);
            foreach (Button tmp in buttons)
            {
                if (tmp.gameObject.name == "TableB")
                {
                    this.EndButton = tmp;
                    break;
                }
            }
    }

    protected virtual void ButtonT()
    {
        if(this.EndButton != null)
        {
            this.EndButton.onClick.AddListener(EndTableScroll);
        }
    }
    protected virtual void EndTableScroll()
    {
        transform.gameObject.SetActive(false);
    }

}
