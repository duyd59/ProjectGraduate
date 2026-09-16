using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyObj : MonoBehaviour
{
    public virtual void Awake()
    {
        this.LoadComponents();
    }
    public virtual void Reset()
    {
        this.LoadComponents();
    }

    public virtual void LoadComponents(){}
    
}
