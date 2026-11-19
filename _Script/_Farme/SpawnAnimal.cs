using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SpawnAnimal : Spawner
{
    [SerializeField]public List<Transform> ListSpawnPoint;
    [SerializeField]public Farme_ScipTble farme_ScipTble;
    [SerializeField]protected EnemyCtl enemyCtl;
    [SerializeField]protected int CurrentPig = 0;
    [SerializeField]protected int CurrentSheep = 0;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemy_ScripTble();
        this.LoadSpawnPoint();
        this.LoadEnemyCtl();

    }

    protected virtual void LoadEnemyCtl()
    {
        if(this.enemyCtl == null)
        {
            Transform Obj = transform.Find("Prefabs");
            foreach(Transform trans in Obj)
            {
                this.enemyCtl = trans.GetComponent<EnemyCtl>();
            }
            
        }
    }
    protected virtual void LoadEnemy_ScripTble(){
        if(this.farme_ScipTble == null)
        {
            string ObjName ="FarmeManager";
            this.farme_ScipTble = Resources.Load<Farme_ScipTble>("_Farme/" + ObjName);
        }
        
    }

    protected virtual void LoadSpawnPoint(){
        if(this.ListSpawnPoint.Count > 0) return;
        Transform Obj = transform.Find("SpawnPoint");
        foreach(Transform trans in Obj)
        {
            this.ListSpawnPoint.Add(trans);
        }
    }

    protected virtual void Update()
    {
        this.SpawnAnimalbyLv();
    }

    protected virtual void SpawnAnimalbyLv()
    {
        if(this.CurrentSheep < this.farme_ScipTble.SheepCount - 4)
        {
            Vector3 Point = this.ListSpawnPoint[0].position;
            Transform Obj = this.ListObj[0];
            this.Spawning(Obj,Point,Obj.rotation);
            this.CurrentSheep++;
        }

        if(this.CurrentPig < this.farme_ScipTble.PigCount)
        {
            Vector3 Point = this.ListSpawnPoint[1].position;
            Transform Obj = this.ListObj[1];
            this.Spawning(Obj,Point,Obj.rotation);
            this.CurrentPig++;
        }
        
    }
}
