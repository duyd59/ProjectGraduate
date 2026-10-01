using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : Spawner
{
    [SerializeField]protected  Vector3 VtSpawn = new Vector3(0f,0f,0f);
    [SerializeField]public Transform CurrentPlayer;


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
        this.CurrentPlayer = this.ListObj[0];
        Transform Player = this.Spawning(this.CurrentPlayer,this.CurrentPlayer.position,this.CurrentPlayer.rotation);
        Cmera_Target.Instance.LoadTarget(Player);
    }
}
