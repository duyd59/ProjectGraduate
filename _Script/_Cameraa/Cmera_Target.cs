using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cmera_Target : MyObj
{
    [SerializeField]protected Transform ObjTarget;
    [SerializeField]protected const float MinX = -8.5f;
    [SerializeField]protected const float MinY = -6.5f;
    [SerializeField]protected const float MaxX = 8.5f;
    [SerializeField]protected const float MaxY = 6.5f;
    public static Cmera_Target Instance;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadInstance();
    }

    protected virtual void LoadInstance()
    {
        if(Cmera_Target.Instance == null) Cmera_Target.Instance = this;
    }


    public virtual void LoadTarget(Transform Obj)
    {
        if(this.ObjTarget == null)
        {
            if (Obj.gameObject.CompareTag("Player"))
            {
                this.ObjTarget = Obj;
            }
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
