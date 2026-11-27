using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Boss1_Ctl : MyObj
{
    [SerializeField]protected Boss1_DameSender boss1_DameSender;
    [SerializeField]protected BoxCollider2D boxCollider2D;
    [SerializeField]public Animator _animator;
    [SerializeField]public Boss1_GManager boss1_GManager;
    [SerializeField]public Boss_ScripTble Boss_ScripTble;
    [SerializeField]protected LayerMask Playermask;
    [SerializeField]protected float range = 0.5f;
    [SerializeField]protected Transform ObjDame;

    [SerializeField]public bool _Issender;
    [SerializeField]protected bool _IsOnDuration;

    [SerializeField]public bool Boss_ismoving;

    [SerializeField]protected Rigidbody2D _rigidbody;
    private float _timeAttack;        

        
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBoxCollider();
        this.LoadAnimations();
        this.LoadDameSender();
        this.LoadTble();
        this.LoadMask();
        this.LoadBossManager();
    }

    protected virtual void LoadMask()
    {
        this.Playermask = LayerMask.GetMask("Player");
    }

    protected virtual void LoadTble()
    {
        if(this.Boss_ScripTble == null)
        {
            this.Boss_ScripTble = Resources.Load<Boss_ScripTble>("_Boss/" + transform.name);
        }
    }


    protected virtual void LoadBoxCollider(){
        if(this.boxCollider2D == null) this.boxCollider2D = transform.GetComponent<BoxCollider2D>();
        if(this._rigidbody ==null) this._rigidbody = transform.GetComponent<Rigidbody2D>();
    }
    protected virtual void LoadAnimations()
    {
        if(this._animator == null) this._animator = transform.GetComponent<Animator>();
    }

    protected virtual void LoadDameSender()
    {
        if(this.boss1_DameSender == null){
            this.boss1_DameSender = transform.GetComponentInChildren<Boss1_DameSender>();
        }
    }
    void Start()
    {
        this.Boss_ismoving = true;
        this._IsOnDuration = false;
    }
        
    void Update()
    {
        this.GetInput();
        this.IsAttack();
        this.SetAni();
    }

    protected virtual void LoadBossManager()
    {
        if(this.boss1_GManager == null) this.boss1_GManager = transform.parent.GetComponent<Boss1_GManager>();
    }

    protected virtual void SetAni()
    {
        this._animator.SetBool("_IsattackRight",this._IsOnDuration);
    }
    private void GetInput()
    {
        this._rigidbody.velocity = Vector2.zero; // Dùng rb.velocity = Vector2.zero nếu ở Unity 2022 trở xuống
        this._rigidbody.angularVelocity = 0f;
        this._timeAttack+=Time.fixedDeltaTime;
        if(this.boss1_GManager.TargetObj == null) return;
        float DistanceAttack = Vector3.Distance(transform.position,this.boss1_GManager.TargetObj.position);

        if(this._timeAttack >= this.Boss_ScripTble._TimeAttack)
        {
            if(DistanceAttack <= 2f)
            {
                this._timeAttack = 0f;
                this._Issender = true;
                this._IsOnDuration = true;
            }
        }
        if (DistanceAttack <= 2f)
        {
            this.Boss_ismoving = false;
        }

        if(DistanceAttack > 2f)
        {
            this.Boss_ismoving = true;
            this._Issender = false;
            this._IsOnDuration = false;
        }
    }
    
    protected virtual bool IsDame()
    {
        float facingDir = Mathf.Sign(transform.localScale.x * 0.25f);
        Vector3 dir = (transform.right*0.5f);
        if(transform.localScale.x == -1)
        {
            dir*=-1;
        }
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(this.boxCollider2D.bounds.center + transform.right* range *facingDir - dir,
        new Vector3(this.boxCollider2D.bounds.size.x,this.boxCollider2D.bounds.size.y,this.boxCollider2D.bounds.size.z),
        0,Vector2.left,0,Playermask);

        if(raycastHit2D.collider != null)
        {
            this.ObjDame = raycastHit2D.transform;
            return true;
        }
        this._IsOnDuration = false;
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
            this.boss1_DameSender.SendDame(this.ObjDame);
        }
    }
}
    
    
    

    

