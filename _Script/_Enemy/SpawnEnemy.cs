using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnEnemy : Spawner
{
    [SerializeField]protected List<Transform> ListSpawnPoint;
    [SerializeField]protected int CountCurrentEnemy;
    [SerializeField]protected EnemyCtl enemyCtl;
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
        if(this.ListEnemy_ScripTble.Count > 0) return;
        foreach(Transform trans in this.ListObj)
        {
            string ObjName = trans.name;
            this.ListEnemy_ScripTble.Add(Resources.Load<Enemy_ScripTble>("_Enemy/" + ObjName));
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

    protected virtual void Start()
    {
        if(SceneManager.GetActiveScene().name == "Scene_Village" || SceneManager.GetActiveScene().name == "Scene_Start") return;
        StartCoroutine(this.SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {

        while (true)
        {
            int Indx = this.GetIndex();
            yield return new WaitForSeconds(this.ListEnemy_ScripTble[Indx].SpawnInterval);
            SpawnMonster(Indx);
        }
    }

    protected virtual void SpawnMonster(int Indx)
    {
        if(this.CountCurrentEnemy < this.ListEnemy_ScripTble[Indx].EnemyCount){
            Vector3 Point = this.GetPoint();
            Transform Obj = this.ListObj[Indx];
            this.enemyCtl.SetSpawnPoint(Point);
            for(int i = 0;i < this.ListEnemy_ScripTble[Indx].EnemyCount; i++)
            {
                this.Spawning(Obj,Point,Obj.rotation);
                this.CountCurrentEnemy++;
            }
        }
    }

    protected virtual Vector3 GetPoint(){
        int Indx = Random.Range(0,this.ListSpawnPoint.Count);
        return this.ListSpawnPoint[Indx].position;        
    }

    protected virtual int GetIndex()
    {
        int Indx = Random.Range(0,this.ListObj.Count);
        return Indx;
    }
}
