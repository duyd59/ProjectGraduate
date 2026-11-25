using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;

public class SpawnTank : Spawner
{ 
    [SerializeField]protected List<int> ListPosX = new List<int>(){-18,18};
    [SerializeField]protected SpawnBullet spawnBullet;
    [SerializeField]protected float TankTimemax = 1f;
    [SerializeField]protected float TankTimeCurrent;
    [SerializeField]protected int TankCountMax = 20;
    [SerializeField]protected int TankCurrent = 0;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent();
    }

    protected virtual void LoadComponent()
    {
        if(this.spawnBullet == null)
        {
            foreach(Transform trans in transform.parent)
            {
                if(trans.name == "Defense")
                {
                    this.spawnBullet = trans.GetComponent<SpawnBullet>();
                }
            }
        }
    }    
    protected virtual void FixedUpdate()
    {
        this.SpawnTankking();
    }

    protected virtual void SpawnTankking()
    {
        if(this.TankTimeCurrent < this.TankTimemax)
        {
            this.TankTimeCurrent+=Time.fixedDeltaTime;
        }

        if(this.TankTimeCurrent >= this.TankTimemax)
        {
            if(this.TankCurrent >= this.TankCountMax) return;
            this.TankTimeCurrent = 0f;
            float PosX = this.ListPosX[Posx()];
            Vector3 TankPos = new Vector3(PosX,Posy(),0f);
            Transform tank = this.Spawning(this.ListObj[0],TankPos,transform.rotation);
            this.spawnBullet.BulletByTank(tank);
            this.TankCurrent++;
            if(TankPos.x > 0)
            {
                tank.localScale = new Vector3(tank.localScale.x*-1,tank.localScale.y,tank.localScale.z);
            }
            
        }
    }

    

    protected virtual int Posx()
    {
        int Idx = UnityEngine.Random.Range(0,this.ListPosX.Count);
        return Idx;
    }

    protected virtual float Posy()
    {
        float Idx =  UnityEngine.Random.Range(-8f,3f);
        return Idx;
    }

    
}
