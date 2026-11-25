using System.Collections;
using System.Collections.Generic;
using Cainos.PixelArtTopDown_Basic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerCtl : MyObj
{
    private HashSet<Collider2D> hitTargets = new HashSet<Collider2D>();
    [SerializeField]protected LocalsaveManager LocalsaveManager;
    [SerializeField]protected bool SecretKey;
    [SerializeField]protected DameSender dameSender;
    [SerializeField]public Player_ScripTble player_ScripTble;
    [SerializeField]protected SpawnPlayer spawnPlayer;
    [SerializeField]public Vector3 SppearPos;
    [SerializeField]protected BoxCollider2D boxCollider2D;
    [SerializeField]protected LayerMask Layermask;
    [SerializeField]protected Transform ObjDame;
    [SerializeField]protected float range = 0.5f;
    [SerializeField]protected bool Canloadscene;
    [SerializeField]protected Transform TArgetScene;
    public bool BossKey = true;
    public static PlayerCtl Instance;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BindCamera();
    }

    public override void LoadComponents()
    {
        this.LoadPlayerCtl();
        base.LoadComponents();
        this.LoadDameSender();
        this.LoadScripTble();
        this.LoadSpawnPlayer();
        this.LoadBoxcollider();
        this.LoadLayermask();
        
    }

    protected virtual void LoadPlayerCtl()
    {
        if(PlayerCtl.Instance == null) PlayerCtl.Instance = this;
    }
    protected virtual void Update()
    {
        this.LoadScene_village();
    }

    protected virtual void LoadLayermask()
    {
        this.Layermask = LayerMask.GetMask("Enemy","Boss");
    }


    protected virtual void LoadBoxcollider(){
        if(this.boxCollider2D == null) this.boxCollider2D = transform.GetComponent<BoxCollider2D>();
    }

    protected virtual void LoadSpawnPlayer()
    {
        if(this.spawnPlayer == null) this.spawnPlayer = transform.parent.GetComponentInParent<SpawnPlayer>();
    }

    protected virtual void LoadDameSender()
    {
        if(this.dameSender == null)
        {
            this.dameSender = transform.GetComponentInChildren<DameSender>();
        }
    }

    protected virtual void LoadScripTble()
    {
        if(this.player_ScripTble == null)
        {
            string LoadPath = "_Player/Player";
            this.player_ScripTble = Resources.Load<Player_ScripTble>(LoadPath);
        }
        if(this.LocalsaveManager == null) this.LocalsaveManager = transform.GetComponent<LocalsaveManager>();
    }

    protected virtual void LoadScene_village()
    {
        if(this.TArgetScene == null) return;
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene(this.TArgetScene.name);
            this.Canloadscene = false;
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("NPC"))
        {
            if(coll.gameObject.name == "Village_Chief")
            {
                Debug.Log("SecretKey");
                this.SecretKey = true;
            }
        }
        
        
        if (coll.gameObject.CompareTag("LoadScene"))
        {
            if(coll.name == "Scene_Village")
            {
                this.SppearPos = new Vector3(0f,0f,0f);
                if(SceneManager.GetActiveScene().name == "Scene_Tutorial")
                {
                    this.Canloadscene = true;
                    this.TArgetScene = coll.transform;
                }
                else if(!(SceneManager.GetActiveScene().name == "Scene_Tutorial"))
                {
                    Debug.Log("4235325");
                    SceneManager.LoadScene(coll.name);

                }
            }
            if(coll.name == "Scene_Tutorial")
            {
                if(this.SecretKey == true)
                {
                    this.SppearPos = new Vector3(-7.5f,0f,0f);
                    SceneManager.LoadScene(coll.name);

                }
            }
            if(coll.name == "Scene_Boss")
            {
                this.SppearPos = new Vector3(1f,-13f,0f);
                SceneManager.LoadScene(coll.name);
            }

            if(coll.name == "Boss_1")
            {
                this.SppearPos = new Vector3(0.2f,-8f,0f);
                this.Canloadscene = true;
                this.TArgetScene = coll.transform;
            }
            if(coll.name == "Boss_2")
            {
                this.SppearPos = new Vector3(0.2f,-8f,0f);
                this.Canloadscene = true;
                this.TArgetScene = coll.transform;
            }
            if(coll.name == "Boss")
            {
                this.SppearPos = new Vector3(0.2f,-8f,0f);
                this.Canloadscene = true;
                this.TArgetScene = coll.transform;
            }

            if(coll.name == "Scene_Farme")
            {
                this.LocalsaveManager.LoadFromJson();
                this.SppearPos = new Vector3(0f,0f,0f);
                SceneManager.LoadScene(coll.name);

            }
            Cmera_Target.Instance.LoadTarget(transform);
        }
    }



    protected virtual bool IsDame()
    {
        float facingDir = Mathf.Sign(transform.localScale.x);
        Vector3 dir = (transform.right*0.5f);
        if(transform.localScale.x == -1)
        {
            dir*=-1;
        }
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(this.boxCollider2D.bounds.center + transform.right* range *facingDir - dir,
        new Vector3(this.boxCollider2D.bounds.size.x,this.boxCollider2D.bounds.size.y,this.boxCollider2D.bounds.size.z),
        0,Vector2.left,0,Layermask);

        if(raycastHit2D.collider != null)
        {
            this.ObjDame = raycastHit2D.transform;
            return true;
        }
        return false;
    }

    protected virtual void OnDrawGizmos()
    {
        float facingDir = Mathf.Sign(transform.localScale.x);
        Vector3 dir = (transform.right*0.5f);
        if(transform.localScale.x == -1)
        {
            dir*=-1;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.boxCollider2D.bounds.center  + (transform.right* range *facingDir - dir),
        new Vector3(this.boxCollider2D.bounds.size.x,this.boxCollider2D.bounds.size.y,this.boxCollider2D.bounds.size.z));
    }

    public virtual void IsAttack()
    {
        if (this.IsDame())
        {
            this.dameSender.SendDame(this.ObjDame);
        }
    }


    protected virtual void ResetHittargets(Collider2D collider2D)
    {
        this.hitTargets.Remove(collider2D);
    }
    

    private void Start()
    {
        BindCamera();
    }

    public void BindCamera()
    {
        if(this.spawnPlayer.CurrentPlayer != null)
        {
            transform.position = this.SppearPos;
        }

        Camera mainCam = Camera.main;

        if (mainCam != null)
        {
            if (mainCam.TryGetComponent<Cmera_Target>(out var camFollow))
            {
                camFollow.LoadTarget(transform);
                
                Vector3 newCamPos = transform.position;
                newCamPos.z = mainCam.transform.position.z; // Giữ nguyên trục Z của Camera
                mainCam.transform.position = newCamPos;
            }
        }
    }
   
}
