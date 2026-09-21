using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MyObj
{
    [SerializeField]protected List<Transform> ListButtons;
    [SerializeField]protected List<EnumType> ListEnums;
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadButtons();
    }
    protected virtual void Start()
    {
        this.LoadOnclick();
    }

    protected virtual void LoadOnclick()
    {
        foreach(Transform trans in this.ListButtons)
        {
            StartButtonCtl startButtonCtl = trans.GetComponent<StartButtonCtl>();
            Button butt = trans.GetComponent<Button>();
            if (butt != null && startButtonCtl != null)
            {
                EnumType currentType = startButtonCtl.ReturnType();
                butt.onClick.AddListener(() => HandleButton(currentType));
            }
        }
    }

    protected virtual void HandleButton(EnumType type)
    {
        switch (type)
        {
            case EnumType.Play:
                SceneManager.LoadScene("Scene_Village");
                break;
            case EnumType.Options:
                Debug.Log("Open Options");
                break;
            case EnumType.Quit:
                Debug.Log("Quit Game");
                break;
        }
    }

    protected virtual void LoadButtons()
    {
        foreach(Transform trans in transform)
        {
            this.ListButtons.Add(trans);
            
        }
    }
}
