using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMoverment : MyObj
{
      [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;        
    [SerializeField] private float MoveRadius = 5;     

    [Header("Idle Settings")]
    [SerializeField] private float minWaitTime = 1.5f;     
    [SerializeField] private float maxWaitTime = 3.5f;     

    [Header("References")]
    [SerializeField] private Vector3 spawnPoint;     
    [SerializeField]protected SpawnEnemy spawnEnemy;   

    private Vector3 targetPosition;
    private bool isMoving = false;
    private Animator Ani;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawnEnemy();
    }

    protected virtual void LoadSpawnEnemy()
    {
        if(this.spawnEnemy== null) this.spawnEnemy = transform.parent.parent.parent.GetComponent<SpawnEnemy>();
    }

    private void Start()
    {

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
            targetPosition = GetRandomPointAroundSpawn();
            this.SetWaitTime();
            
            isMoving = true;

            //if (animator != null) animator.SetBool("IsMoving", true);

            yield return new WaitUntil(() => !isMoving);
        }
    }

    private Vector3 GetRandomPointAroundSpawn()
    {
        Vector3 randomCircle = new Vector3(GetX(),GetY(),0f);
        return randomCircle;
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

    protected virtual float GetX()
    {
        if(transform.parent.name == "Pig")
        {
            float PigX = Random.Range(-16f,-7f);
            return PigX;
        }
        float x = Random.Range(3f,16f);
        return x;
    }
    protected virtual float GetY()
    {
        if(transform.parent.name == "Pig")
        {
            float PigX = Random.Range(-7f,-2f);
            return PigX;
        }
        float y = Random.Range(-7f,-1f);
        return y;
    }

    protected virtual void SetWaitTime(){
        this.minWaitTime = 1.5f;
        this.maxWaitTime = 3.5f;
    } 
}
