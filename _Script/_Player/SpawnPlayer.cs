using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPlayer : Spawner
{
    [SerializeField]protected  Vector3 VtSpawn = new Vector3(0f,0f,0f);
    [SerializeField]public Transform CurrentPlayer;
    [SerializeField]protected PlayerCtl playerCtl;
    [SerializeField]public static bool _IsFamer = true;
    public static SpawnPlayer Instance;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawnPlayer();
        this.LoadPlayerCtl();
        this.LoadScene();
    }

    protected virtual void LoadSpawnPlayer()
    {
        if(SpawnPlayer.Instance == null) SpawnPlayer.Instance = this;
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

    protected virtual void LoadPlayerCtl()
    {
        Transform Obj = transform.Find("Prefabs");
        if(this.playerCtl == null) this.playerCtl = Obj.GetComponentInChildren<PlayerCtl>(true);
    }

    protected virtual void Start()
    {
        this.SpawnObj();
    }

    protected virtual void Update()
    {
        this.ChangeFamer();
    }
   
    protected virtual void ChangeFamer()
    {
        if(SceneManager.GetActiveScene().name == "Scene_Farme")
        {
            Debug.Log("34345");
            Destroy(transform.gameObject);
            return;
        }
    }

    
    public virtual void SpawnObj()
    {
        this.CurrentPlayer = this.ListObj[0];
        
        Vector3 Pos = this.GetPos();
        Transform Player = this.Spawning(this.CurrentPlayer,this.playerCtl.SppearPos,this.CurrentPlayer.rotation);
        if(Player != null)
        {
            Player.position = Pos;
        }
        Cmera_Target.Instance.LoadTarget(Player);
    }

    protected virtual Vector3 GetPos()
    {
        float loadX = PlayerPrefs.GetFloat("Spawn_X");
        float loadY = PlayerPrefs.GetFloat("Spawn_Y");
        float loadZ = PlayerPrefs.GetFloat("Spawn_Z");
        Vector3 spawnPosition = new Vector3(loadX, loadY, loadZ);
        PlayerPrefs.DeleteKey("Spawn_X");
        PlayerPrefs.DeleteKey("Spawn_Y");
        PlayerPrefs.DeleteKey("Spawn_Z");
        return spawnPosition;
    }
}
