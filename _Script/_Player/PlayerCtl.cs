using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCtl : MyObj
{
    [SerializeField]protected DameSender dameSender;
    [SerializeField]public Player_ScripTble player_ScripTble;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDameSender();
        this.LoadScripTble();
        this.LoadScene();
    }

    protected virtual void LoadScene()
    {
        DontDestroyOnLoad(transform.gameObject);
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
        }
        if (!(coll.gameObject.CompareTag("Enemy"))) return;
    }
   
}
