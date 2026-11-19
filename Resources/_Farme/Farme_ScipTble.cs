using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
[CreateAssetMenu(fileName = "Farme_ScipTble",menuName = "SO/Farme_ScipTble")]
public class Farme_ScipTble : ScriptableObject
{
    public int Flower;
    public int Meat;
    public int Fur;
    public int Experience = 1;
    public int SheepCount;
    public int PigCount;
    public int CurrentLv = 0;
    public int MaxLv;
    public float CurrentXP;
    public float MaxXp;
    private int BaseXp = 10;
    private float Multiplier = 1.5f;

    public void SetMaxXp()
    {
        this.CurrentXP = this.MaxXp;
        this.CurrentLv++;
        this.MaxXp = this.BaseXp *Mathf.Pow(this.Multiplier,(float)this.CurrentLv - 1);
    }
}
