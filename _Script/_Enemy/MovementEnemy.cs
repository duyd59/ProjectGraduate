using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementEnemy : MyObj
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;        
    [SerializeField] private float MoveRadius = 3f;     

    [Header("Idle Settings")]
    [SerializeField] private float minWaitTime = 1.5f;     
    [SerializeField] private float maxWaitTime = 3.5f;     

    [Header("References")]
    [SerializeField] private Vector3 spawnPoint;        

    private Vector3 targetPosition;
    protected Transform TargetObj;
    private bool isMoving = false;
    private EnemyCtl enemyCtl;
    private Animator Ani;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyCtl();
    }

    protected virtual void LoadEnemyCtl()
    {
        if(this.enemyCtl == null)
        {
            this.enemyCtl = transform.GetComponentInParent<EnemyCtl>();
        }
    }

    private void Start()
    {
        this.spawnPoint = enemyCtl.EnemyPoint;

        StartCoroutine(WanderRoutine());
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.parent.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);


            if (Vector3.Distance(transform.position, targetPosition) < 0.5f)
            {
                isMoving = false;
                //if (animator != null) animator.SetBool("IsMoving", false);
            }
            this.HandleFacing();
        }
    }

    private IEnumerator WanderRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(waitTime);

            this.GetTargetObj();
            Debug.Log(TargetObj);
            if(this.TargetObj == null)
            {
                targetPosition = GetRandomPointAroundSpawn();
                this.SetWaitTime();
            }
            if(this.TargetObj != null){
                minWaitTime = 0;
                maxWaitTime = 0;
                targetPosition = this.TargetObj.position;
            }
            
            isMoving = true;

            //if (animator != null) animator.SetBool("IsMoving", true);

            yield return new WaitUntil(() => !isMoving);
        }
    }

    private Vector3 GetRandomPointAroundSpawn()
    {
        Vector2 randomCircle = Random.insideUnitCircle * MoveRadius;
        return spawnPoint + new Vector3(randomCircle.x, randomCircle.y, 0f);
    }

    private void HandleFacing()
    {
        Vector3 FacePos = transform.parent.localScale;
        if((this.targetPosition.x - transform.parent.position.x) < 0 && transform.parent.localScale.x >0)
        {
            transform.parent.localScale = new Vector3(FacePos.x*-1,FacePos.y,FacePos.z);
        }
        if((this.targetPosition.x - transform.parent.position.x)> 0 && transform.parent.localScale.x <0)
        {
            transform.parent.localScale = new Vector3(FacePos.x*-1,FacePos.y,FacePos.z);;
        }
    }

    protected virtual void GetTargetObj()
    {
        this.TargetObj = enemyCtl.FindTarget;
    }

    protected virtual void SetWaitTime(){
        this.minWaitTime = 1.5f;
        this.maxWaitTime = 3.5f;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 center = (spawnPoint != null) ? spawnPoint : transform.position;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(center, MoveRadius);
    }
}

