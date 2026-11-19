using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Boss1_Ctl : MyObj
{
    [SerializeField]protected Boss1_DameSender boss1_DameSender;
    [SerializeField]protected CircleCollider2D circleCollider2D;
    [SerializeField]protected BoxCollider2D boxCollider2D;
    [SerializeField]public Animator _animator;
    [SerializeField]public Boss1_GManager boss1_GManager;
    [SerializeField]public Boss_ScripTble Boss_ScripTble;
    [SerializeField]protected LayerMask Playermask;
    [SerializeField]protected float range = 0.5f;
    [SerializeField]protected Transform ObjDame;
    [Header("Melee Attack")]
    [SerializeField] private bool onlyMeleeAttackOnGround = false;
    [SerializeField]public bool _Issender;
    [SerializeField]protected bool _IsOnDuration;
    [SerializeField] private int maxMeleeAttackComboCount = 1;
    [SerializeField] private float maxMeleeComboExpireTimer = 0.2f;
    [SerializeField] private float maxMeleeAttackInputBuffer = 0.2f;
    [SerializeField] private float meleeAttackRecoveringTimer = 0.5f;
    [Header("Range Attack")]
    [SerializeField] private bool onlyRangeAttackOnGround = false;
    [SerializeField] private float rangedAttackRecoveringTimer = 0.3f;

    [SerializeField]public bool Boss_ismoving;

    [SerializeField]protected SpriteRenderer _spriteRenderer;
    [SerializeField]protected Rigidbody2D _rigidbody;
    // Melee Attack
    private bool _isMeleeAttacking = false;
    public bool _meleeAttack = false;
    private int _meleeAttackComboCount = 0;
    private float _meleeComboExpireTimer = 0f;
    private float _meleeAttackInputBufferTimer = 0f;
    private float _timeAttack;
    private bool _IsAttack;
        
    // Range Attack
    private bool _rangeAttack = false;

        
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAnimations();
        this.LoadDameSender();
        this.LoadCollider2D();
        this.LoadTble();
        this.LoadMask();
        this.LoadBoxCollider();
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

    protected virtual void LoadCollider2D()
    {
        if(this.circleCollider2D == null) this.circleCollider2D = transform.GetComponent<CircleCollider2D>();
    }

    protected virtual void LoadBoxCollider(){
        if(this.boxCollider2D == null) this.boxCollider2D = transform.GetComponent<BoxCollider2D>();
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
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        this.Boss_ismoving = true;
    }
        
    void Update()
    {
        GetInput();
        HandleAnimationsParameters();
        this.IsAttack();
        //HandleRangeAttack();
        //HandleMeleeAttack();
    }

    protected virtual void LoadBossManager()
    {
        if(this.boss1_GManager == null) this.boss1_GManager = transform.parent.GetComponent<Boss1_GManager>();
    }

       

    private void GetInput()
    {
        this._timeAttack+=Time.fixedDeltaTime;
        if(this.boss1_GManager.TargetObj == null) return;
        float DistanceAttack = Vector3.Distance(transform.position,this.boss1_GManager.TargetObj.position);

        if(this._timeAttack >= this.Boss_ScripTble._TimeAttack)
        {
            if(DistanceAttack <= 4f)
            {
                this._meleeAttack = true;
                this._timeAttack = 0f;
                this._isMeleeAttacking = true;
                if (this._IsOnDuration)
                {
                    this._Issender = true;
                }
            }
        }
        if (DistanceAttack <= 2f)
        {
            this.Boss_ismoving = false;
            
        }

        if(DistanceAttack > 4f)
        {
            this.Boss_ismoving = true;
            this._Issender = false;
        }
    }

        
    private void HandleAnimationsParameters()
    {
        _animator.SetInteger("meleeAttackComboCount", _meleeAttackComboCount);

        if (_rangeAttack)
        {
            _animator.SetTrigger("rangeAttack");
            _rangeAttack = false;
        }

        if (_meleeAttack)
        {
            _animator.SetTrigger("meleeAttack");
            _meleeAttack = false;
        }
        this._animator.SetBool("isHorizontalMoving",this.Boss_ismoving);
    }
        
    private void HandleRangeAttack(){
        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
            
        InterruptMeleeAttacking();
        _rangeAttack = true;
    }
        

    private void InterruptRangeAttacking()
    {
        _rangeAttack = false;
    }

        
    private void HandleMeleeAttack()
    {
        // We always decrease the meleeComboExpireTimer. This timer is used to know when a combo had expired
        if (_meleeComboExpireTimer > 0) _meleeComboExpireTimer -= Time.deltaTime;

        // We always decrease the meleeAttackBuffer. This timer is used to buff the attack button press and 
        // give the player a better response for attack button pressing
        if (_meleeAttackInputBufferTimer > 0) _meleeAttackInputBufferTimer -= Time.deltaTime;
            
        // If we are already attacking we do nothing here
        if (_isMeleeAttacking) return;
            
        // If we have no melee attack input and we have no input buffered, we do nothing here
            
        // If we can only melee attack on ground and we are not grounded we do nothing here

        if (_meleeComboExpireTimer <= 0)
        {
            _meleeAttackComboCount = 0;
        }

            
        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        _meleeAttack = true;
        _isMeleeAttacking = true;

    }
        
        /**
         * Called by other states that may need to interrupt the meleeAttack before it reaches the finishing point
         */
    private void InterruptMeleeAttacking()
    {
        _meleeAttack = false;
        _isMeleeAttacking = false;
        _meleeAttackComboCount = 0;
        _meleeComboExpireTimer = 0;
        _meleeAttackInputBufferTimer = 0;
    }

        /**
         * Function to be called by character's animator component to notify the script
         * that the currently executing attack had finished
         * In the character "Animation" menu, on the melee attacks animations, there's an "event"
         * that calls this function at the point we want the character to be able to connect
         * the next attack combo
         */
    private void FinishMeleeAttacking()
    {
        _isMeleeAttacking = false;
        _animator.ResetTrigger("meleeAttack");
        _meleeComboExpireTimer = maxMeleeComboExpireTimer;
        _meleeAttackComboCount =
        _meleeAttackComboCount + 1 > maxMeleeAttackComboCount ? 0 : _meleeAttackComboCount + 1;
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
            this._IsOnDuration = true;
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
            Debug.Log("1234");
            this.boss1_DameSender.SendDame(this.ObjDame);
        }
    }
}
    
    
    

    

