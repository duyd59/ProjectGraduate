using System.Collections;
using System.Collections.Generic;
using Cainos.PixelArtTopDown_Basic;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerCtl : MyObj
{
    [SerializeField]protected DameSender dameSender;
    [SerializeField]public Player_ScripTble player_ScripTble;
    [SerializeField]protected GameObject KeyTutorial;
    [SerializeField]protected SpawnPlayer spawnPlayer;
    [SerializeField]protected Vector3 SppearPos;
    public static PlayerCtl Instance { get; private set; }
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
        base.LoadComponents();
        this.LoadDameSender();
        this.LoadScripTble();
        this.LoadSpawnPlayer();
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
    }

    protected virtual void OnTriggerEnter2D(Collider2D coll)
    {
        this.dameSender.SendDame(coll.transform);
        if (coll.gameObject.CompareTag("LoadScene"))
        {
            if(coll.name == "Scene_Tutorial")
            {
                if(this.KeyTutorial == null)
                {
                    SceneManager.LoadScene(coll.name);
                    this.SppearPos = new Vector3(-7.5f,0f,0f);
                }
                else
                {
                    SceneManager.LoadScene(coll.name);
                    this.SppearPos = new Vector3(-7.5f,0f,0f);
                }
            }
            else
            {
                SceneManager.LoadScene(coll.name);
                Cmera_Target.Instance.LoadTarget(transform);
                this.SppearPos = new Vector3(-15f,0f,0f);
            }
        }
        if (!(coll.gameObject.CompareTag("Enemy"))) return;
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
        transform.position = new Vector3(0f,0f,0f);

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
