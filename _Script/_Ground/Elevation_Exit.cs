using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevation_Exit : MyObj
{
    [SerializeField]protected List<Collider2D> ColliderList;
    [SerializeField]protected List<Collider2D> Boundary_ColliderList;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadColliders();
    }

    protected virtual void LoadColliders()
    {
        if(this.ColliderList.Count <= 0)
        {
            GameObject Obj = GameObject.Find("Grid");
            foreach(Transform trans in Obj.transform)
            {
                if(trans.GetComponent<Collider2D>() != null && !(trans.GetComponent<Collider2D>() is BoxCollider2D))
                {
                    if(trans.name == "MountainBoundary")
                    {
                        this.Boundary_ColliderList.Add(trans.GetComponent<Collider2D>());
                        trans.GetComponent<Collider2D>().enabled = false;
                    }
                    else
                    {
                        this.ColliderList.Add(trans.GetComponent<Collider2D>());
                    }
                }
            }
        }
    }

    

    protected virtual void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.CompareTag("Player"))
        {
            foreach(Collider2D collider in this.ColliderList)
            {
                collider.enabled = true;
            }
        
            foreach(Collider2D collider in this.Boundary_ColliderList)
            {
                collider.enabled = false;
            }
            coll.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;
        }
        
    }
}
