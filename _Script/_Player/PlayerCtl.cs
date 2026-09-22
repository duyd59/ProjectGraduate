using System.Collections;
using System.Collections.Generic;
using Cainos.PixelArtTopDown_Basic;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCtl : MyObj
{
    [SerializeField]protected DameSender dameSender;
    [SerializeField]public Player_ScripTble player_ScripTble;
    public static PlayerCtl Instance { get; private set; }

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDameSender();
        this.LoadScripTble();
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
        this.dameSender.SendDame(transform);
        if (coll.gameObject.CompareTag("LoadScene"))
        {
            SceneManager.LoadScene(coll.name);
            Cmera_Target.Instance.LoadTarget(transform);
        }
        if (!(coll.gameObject.CompareTag("Enemy"))) return;
    }

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

    private void Start()
    {
        BindCamera();
    }

    public void BindCamera()
    {
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
