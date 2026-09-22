using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : Spawner
{
    [SerializeField]protected  Vector3 VtSpawn = new Vector3(0f,0f,0f);
    public static SpawnPlayer Instance { get; private set; }

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadScene();
    }

    protected virtual void LoadScene()
    {   
        if (Instance != null && Instance != this)
        {
            Destroy(transform.gameObject);
            return;
        }
        SpawnPlayer.Instance = this;
        DontDestroyOnLoad(transform.gameObject);
    }

    protected virtual void Start()
    {
        this.SpawnObj();
    }

    protected virtual void SpawnObj()
    {
        Transform Obj = this.ListObj[0];
        Transform Player = this.Spawning(Obj,Obj.position,Obj.rotation);
        Cmera_Target.Instance.LoadTarget(Player);
    }
}
