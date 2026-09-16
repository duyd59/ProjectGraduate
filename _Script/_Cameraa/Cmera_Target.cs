using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cmera_Target : MyObj
{
    [SerializeField]protected GameObject ObjTarget;
    [SerializeField]protected const float MinX = -8.5f;
    [SerializeField]protected const float MinY = -6.5f;
    [SerializeField]protected const float MaxX = 8.5f;
    [SerializeField]protected const float MaxY = 6.5f;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTarget();
    }

    protected virtual void LoadTarget()
    {
        if(this.ObjTarget == null)
        {
            this.ObjTarget = GameObject.FindGameObjectWithTag("Player");
        }
    }

    protected virtual void Update()
    {
        this.CameraMoving();
    }

    protected virtual void CameraMoving()
    {
        Vector3 CamPos = this.ObjTarget.transform.position;
        CamPos.z = -10;
        if(CamPos.x >= MaxX)
        {
            CamPos.x = MaxX;
        }
        if(CamPos.x <= MinX)
        {
            CamPos.x = MinX;
        }
        if(CamPos.y >= MaxY)
        {
            CamPos.y = MaxY;
        }
        if(CamPos.y <= MinY)
        {
            CamPos.y = MinY;
        }

        transform.position= Vector3.Lerp(transform.position,CamPos,5f);
    }
}
