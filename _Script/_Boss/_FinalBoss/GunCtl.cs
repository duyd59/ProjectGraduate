using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunCtl : MyObj
{
    [SerializeField]protected Boss1_GManager boss1_GManager;
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent();
    }

    protected virtual void LoadComponent()
    {
        if(this.boss1_GManager == null)
        {
            this.boss1_GManager = transform.parent.GetComponentInParent<Boss1_GManager>();
        }
    }
    protected virtual void Update()
    {
        this.Rotating();
    }

    protected virtual void Rotating()
    {
        if(this.boss1_GManager.TargetObj != null){
            Vector3 direction = this.boss1_GManager.TargetObj.position - transform.position;
            if (direction.sqrMagnitude < 0.0001f) return;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if(transform.position.x > 0) angle+=180f; 
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                25f * Time.deltaTime
            );
        }
    }
}
