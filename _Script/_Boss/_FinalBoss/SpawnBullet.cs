using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnBullet : Spawner
{
    [SerializeField]protected Boss1_GManager boss1_GManager;
    [SerializeField]public Vector3 BulletPos1 = new Vector3(16.5f,9f,0f);
    [SerializeField]public Vector3 BulletPos2 = new Vector3(-16.5f,9f,0f);
    [SerializeField]protected float ShootTimeMax = 5f;
    [SerializeField]protected float ShootTimeCurrent;
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent();
    }

    protected virtual void LoadComponent()
    {
        if(this.boss1_GManager == null)
        {
            this.boss1_GManager = transform.parent.GetComponent<Boss1_GManager>();
        }
    }

    

    protected virtual void FixedUpdate()
    {
        if(this.ShootTimeCurrent < this.ShootTimeMax)
        {
            this.ShootTimeCurrent+=Time.fixedDeltaTime;
        }
        else
        {
            this.Shootting();
        }
    }

    protected virtual void Shootting()
    {
        
        if(this.boss1_GManager.TargetObj.position.y >= 4f)
        {
            Spawning(this.ListObj[0],BulletPos1,transform.rotation);
            Spawning(this.ListObj[0],BulletPos2,transform.rotation);
            this.ShootTimeCurrent = 0f;
        }
    }
}
