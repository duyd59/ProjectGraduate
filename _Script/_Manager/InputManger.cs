using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class InputManger : MyObj
{
    public static InputManger Instance;
    [SerializeField]protected float horizontal;
    public float Horizontal => horizontal;

    [SerializeField]protected float vertical;
    public float Vertical => vertical;
    [SerializeField]protected bool leftMouse;
    public bool LeftMouse => leftMouse;
    [SerializeField]public bool DirectionDame = false;
    [SerializeField]protected bool rightMouse;
    public bool RightMouse => rightMouse;
    [SerializeField]protected bool dig;
    public bool Dig => dig;
    [SerializeField]protected bool tab;
    public bool Tab => tab;

    [SerializeField]protected bool plant;
    public bool Plant => plant;
    [SerializeField]protected bool harvest;
    public bool Harvest => harvest;

    public override void Awake()
    {
        base.Awake();
        this.LoadInstance();
    }

    protected virtual void LoadInstance()
    {
        if(InputManger.Instance == null) InputManger.Instance = this;
    }

    protected virtual void Update()
    {
        this.LoadUnitMovement();
        this.LoadMouse();
        this.LoadDig();
        this.LoadPlant();
        this.LoadHarvesrt();
        this.LoadTab();
    }

    protected virtual void LoadUnitMovement()
    {
        this.horizontal = Input.GetAxis("Horizontal");
        this.vertical  = Input.GetAxis("Vertical");
    }

    protected virtual void LoadMouse()
    {
        if (Input.GetMouseButtonDown(0) && this.leftMouse == false)
        {
            this.leftMouse = true;
            this.DirectionDame = false;
        }

        if(Input.GetMouseButtonUp(0)  && this.leftMouse == true) {
            this.leftMouse = false;
            this.DirectionDame = true;
        }
        if (Input.GetMouseButtonDown(1) && this.rightMouse == false)
        {
            this.rightMouse = true;
            this.DirectionDame = false;
        }

        if(Input.GetMouseButtonUp(1)){ 
            this.rightMouse = false;
            this.DirectionDame = true;
        }
    }

    protected virtual void LoadDig()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            this.dig = true;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            this.dig = false;
        }
    }

    protected virtual void LoadTab()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            this.tab = true;
        }
    }

    protected virtual void LoadHarvesrt()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            this.harvest = true;
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            this.harvest = false;
        }
    }

    protected virtual void LoadPlant()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            this.plant = true;
        }
        if (Input.GetKeyUp(KeyCode.V))
        {
            this.plant = false;
        }
    }

    public  virtual void SetTab()
    {
        this.tab = false;
    }
}
