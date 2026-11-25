using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTank : DestroyTankCtl
{
    [SerializeField]protected Sprite spriteStart;
    [SerializeField]protected Sprite spriteFinish;
    [SerializeField]protected SpriteRenderer spriteRenderer;
    [SerializeField]protected float TimeBMax = 2.5f;
    [SerializeField]protected float TimeBCurrent = 2.5f;
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSprite();
    }

    protected virtual void LoadSprite()
    {
        if(this.spriteStart == null)
        {
            this.spriteStart = Resources.Load<Sprite>("_Tank/BStart");
        }
        if(this.spriteFinish == null)
        {
            this.spriteFinish = Resources.Load<Sprite>("_Tank/BFinish");
        }
        if(this.spriteRenderer == null)
        {
            this.spriteRenderer = transform.GetComponent<SpriteRenderer>();
            this.spriteRenderer.sprite = this.spriteStart;
        }
        
    }
    
    protected virtual void FixedUpdate()
    {
        this.ControlTime();
    }
    protected virtual void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            if(this.spriteRenderer.sprite != this.spriteFinish)
            {
                this.spriteRenderer.sprite = this.spriteFinish;
                this.TimeBCurrent = 0f;
                countDestroy++;
            }
            
        }
    }

    protected virtual void ControlTime()
    {
        
        if(this.TimeBCurrent < this.TimeBMax)
        {
            this.TimeBCurrent+=Time.fixedDeltaTime;
            if(this.TimeBCurrent >= this.TimeBMax)
            {
                this.spriteRenderer.sprite = this.spriteStart;
                countDestroy--;
            }
        }
    }

}
