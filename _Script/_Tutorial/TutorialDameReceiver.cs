using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDameReceiver : MyObj
{
    [SerializeField]protected bool Isdead;
    [SerializeField]protected TutorialCtl tutorialCtl;

    public override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadTutorialCtl();
    }

    protected virtual void LoadTutorialCtl(){
        if(this.tutorialCtl == null) this.tutorialCtl = transform.GetComponentInParent<TutorialCtl>();
    }
    protected virtual void Update()
    {
        this.CheckStatus();
    }

    public virtual void AddHp(float Hp)
    {
        
    }

    protected virtual void CheckStatus()
    {
        if(this.Isdead == true)
        {
            Debug.Log("you dead");
            this.Isdead= false;
        }
    }
}
