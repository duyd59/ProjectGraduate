using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnTutorial : Spawner
{
    [SerializeField]protected List<Transform> ListSpawnPoint;
    [SerializeField]protected int CountCurrentEnemy;
    [SerializeField]protected TutorialCtl tutorialCtl;
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemy_ScripTble();
        this.LoadSpawnPoint();
        this.LoadEnemyCtl();

    }

    protected virtual void LoadEnemyCtl()
    {
        if(this.tutorialCtl == null)
        {
            Transform Obj = transform.Find("Prefabs");
            foreach(Transform trans in Obj)
            {
                
                this.tutorialCtl = trans.GetComponent<TutorialCtl>();
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
        foreach(Transform trans in this.ListSpawnPoint)
        {
            if(this.CountCurrentEnemy < this.ListSpawnPoint.Count){
                Vector3 Point = trans.position;
                Transform Obj = this.ListObj[Indx];
                //this.tutorialCtl.SetSpawnPoint(Point);
                for(int i = 0;i < this.ListEnemy_ScripTble[Indx].EnemyCount; i++)
                {
                    this.Spawning(Obj,Point,Obj.rotation);
                    this.CountCurrentEnemy++;
                }
            }
        }
        
    }

    protected virtual int GetIndex()
    {
        int Indx = Random.Range(0,this.ListObj.Count);
        return Indx;
    }
}
