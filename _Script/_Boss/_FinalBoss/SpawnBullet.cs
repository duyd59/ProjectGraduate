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
    [SerializeField]protected float ShootTimeMax = 1f;
    [SerializeField]public Transform Postank;
    [SerializeField]protected float ShootTimeCurrent;
    private Coroutine activeRoutine;
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
        
        if(this.boss1_GManager.TargetObj.position.y >= 2f)
        {
            Spawning(this.ListObj[0],BulletPos1,transform.rotation);
            Spawning(this.ListObj[0],BulletPos2,transform.rotation);
            this.ShootTimeCurrent = 0f;
        }
    }

    public virtual void BulletByTank(Transform tank)
    {
        this.Postank = tank;
        Transform Bullettank = this.Spawning(this.ListObj[1],tank.position,tank.rotation);
        // if(tank.position.x < 0)
        // {
        //     Bullettank.localScale = new Vector3(tank.localScale.x*-1,tank.localScale.y,tank.localScale.z);
        // }
        activeRoutine = StartCoroutine(ActionRoutine(tank));
    }
    private IEnumerator ActionRoutine(Transform tank)
    {
        yield return new WaitForSeconds(2f);
        this.BulletByTank(tank);
    }
    private void OnDisable()
    {
        if (activeRoutine != null)
        {
            StopCoroutine(activeRoutine);
            activeRoutine = null;
        }
    }
}
