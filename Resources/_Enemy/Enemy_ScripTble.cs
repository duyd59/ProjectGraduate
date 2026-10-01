using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Enemy_ScripTble",menuName = "SO/Enemy_ScripTble")]
public class Enemy_ScripTble : ScriptableObject
{
    public int Level = 1;
    public float CurrentHp =100f;
    public float Dmae = 2f;
    public float MaxHp = 100f;
    public float Amor = 10f;
    public int EnemyCount = 1;
    public float SpawnInterval;

    public virtual void ResetInfo()
    {
        this.Level = 1;
        this.CurrentHp = MaxHp;
        this.Amor = 10f;
    }
}
