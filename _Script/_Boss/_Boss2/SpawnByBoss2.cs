using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnByBoss2 : Spawner
{
    [SerializeField]protected float TimeSpawner = 5f;
    [SerializeField]protected float CurrenTime = 0f;
    [SerializeField]protected List<Transform> ListPoint;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPoint();
    }

    protected virtual void LoadPoint()
    {
        if(this.ListPoint.Count > 0) return;
        foreach(Transform trans in transform)
        {
            if(trans.name == "SpawnPoint")
            {
                foreach(Transform transform in trans)
                {
                    this.ListPoint.Add(transform);
                }
            }
        }
    }
    protected virtual void FixedUpdate()
    {
        this.SpawnByTime();
    }

    protected virtual void SpawnByTime()
    {
        if(this.CurrenTime < this.TimeSpawner)
        {
            this.CurrenTime+= Time.fixedDeltaTime;
        }
        if(this.CurrenTime >= this.TimeSpawner)
        {
            this.CurrenTime = 0f;
            this.Spawning(this.GetObj(),this.GetPos(),transform.rotation);
        }
    }

    protected virtual Vector3 GetPos()
    {
        int Idx = Random.Range(0,ListPoint.Count);
        return this.ListPoint[Idx].position;
    }
    protected virtual Transform GetObj()
    {
        int Idx = Random.Range(0,ListObj.Count);
        return this.ListObj[Idx];
    }
}
