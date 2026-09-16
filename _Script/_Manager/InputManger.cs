using System.Collections;
using System.Collections.Generic;
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
    [SerializeField]protected bool rightMouse;
    public bool RightMouse => rightMouse;

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
    }

    protected virtual void LoadUnitMovement()
    {
        this.horizontal = Input.GetAxis("Horizontal");
        this.vertical  = Input.GetAxis("Vertical");
    }

    protected virtual void LoadMouse()
    {
        if (Input.GetMouseButton(0))
        {
            this.leftMouse = true;
        }

        if(Input.GetMouseButtonUp(0)) this.leftMouse = false;
        if (Input.GetMouseButton(1))
        {
            this.rightMouse = true;
        }

        if(Input.GetMouseButtonUp(1)) this.rightMouse = false;
    }
}
