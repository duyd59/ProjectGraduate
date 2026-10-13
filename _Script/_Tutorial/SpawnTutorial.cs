using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnTutorial : Spawner
{
    [SerializeField]protected List<Transform> ListSpawnPoint;
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemy_ScripTble();
        this.LoadSpawnPoint();
    }

    public override void Awake()
    {
        base.Awake();
        this.LoadSpawnPoint();
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
            this.DicSpawnObj[trans] = null;

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
        if (this.CountCurrentObj >= this.ListEnemy_ScripTble[Indx].EnemyCount) return;
        foreach (Transform trans in this.ListSpawnPoint)
        {
            if (this.DicSpawnObj[trans] == null)
            {
                Vector3 Point = trans.position;
                Transform Obj = this.ListObj[Indx];

                Transform DicValue = this.Spawning(Obj, Point, Obj.rotation);
                this.DicSpawnObj[trans] = DicValue;
                //DicValue.gameObject.SetActive(true);
                this.CountCurrentObj++;

                break; 
            }
        }
        
    }

    protected virtual int GetIndex()
    {
        int Indx = Random.Range(0,this.ListObj.Count);
        return Indx;
    }
}
