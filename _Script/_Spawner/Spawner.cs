using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MyObj
{
    [SerializeField]protected List<Transform> ListObj;
    [SerializeField]protected List<Transform> ListLoop;
    [SerializeField]protected Transform Holder;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPrefabs();
        this.LoadHolder();
    }

    protected virtual void LoadPrefabs()
    {
        if(this.ListObj.Count <= 0){
            Transform Obj = transform.Find("Prefabs");
            foreach(Transform trans in Obj)
            {
                this.ListObj.Add(trans);
            }
        }

        this.EndLoadPrefabs();
    }

    protected virtual void EndLoadPrefabs()
    {
        foreach(Transform trans in this.ListObj)
        {
            trans.gameObject.SetActive(false);
        }
    }

    protected virtual void LoadHolder()
    {
        if(this.Holder == null)
        {
            this.Holder = transform.Find("Holder");
        }
    }


    protected virtual Transform Spawning(string ObjName,Vector3 ObjPos,Quaternion ObjRotation)
    {
        Transform Obj = this.CheckByName(ObjName);
        if(Obj == null) return null;
        Transform NewObj = this.Spawning(Obj,ObjPos,ObjRotation);
        return NewObj;
    }

    protected virtual Transform Spawning(Transform Obj,Vector3 ObjPos,Quaternion ObjRotation)
    {
        if(this.ListLoop.Count > 0)
        {
            foreach(Transform trans in this.ListLoop)
            {
                if(trans == Obj)
                {
                    trans.position = ObjPos;
                    trans.rotation = ObjRotation;
                    this.ListLoop.Remove(trans);
                    return trans;
                }
            }
        }

        Transform NewObj = Instantiate(Obj,ObjPos,ObjRotation);
        NewObj.name = Obj.name;
        NewObj.SetParent(this.Holder);
        NewObj.gameObject.SetActive(true);
        return NewObj;
    }

    protected virtual Transform CheckByName(string Name)
    {
        foreach(Transform trans in this.ListObj)
        {
            if(trans.name == Name)
            {
                return trans;
            }
        }

        return null;
    }
}
