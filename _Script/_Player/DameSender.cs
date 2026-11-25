using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class DameSender : DameManager
{
    public virtual void SendDame(Transform Enemy)
    {
        EnemyDameReceiver ObjDame = Enemy.GetComponentInChildren<EnemyDameReceiver>();
        Boss1_DameReceiver DameBoss = Enemy.GetComponentInChildren<Boss1_DameReceiver>();
        Boss2EDameREceiver boss2EDameREceiver = Enemy.GetComponentInChildren<Boss2EDameREceiver>();
        if(ObjDame != null)
        {
            if(InputManger.Instance.RightMouse == true || InputManger.Instance.LeftMouse == true)
            {
                ObjDame.DeductHP();
            }
        }

        if(DameBoss != null)
        {
            if(InputManger.Instance.RightMouse == true || InputManger.Instance.LeftMouse == true)
            {
                DameBoss.Deduct(1f);
            }
        }
        if(boss2EDameREceiver != null)
        {
            if(InputManger.Instance.RightMouse == true || InputManger.Instance.LeftMouse == true)
            {

                boss2EDameREceiver.DeductHP(1f);
            }
        }

       
    }
}
