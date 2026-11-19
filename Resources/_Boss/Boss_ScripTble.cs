using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boss_ScripTble",menuName = "SO/Boss_ScripTble")]
public class Boss_ScripTble : ScriptableObject
{
    public float MaxHp;
    public float Dame;
    public float CurrentHp;
    public float Amor;
    public float _TimeAttack = 5f;
    public float _TimeSkill = 10f;
    public float _TimeMax = 30f;

    public virtual void ResetInfo()
    {
        this.CurrentHp = MaxHp;
        this.Amor = 0f;
    }
}
