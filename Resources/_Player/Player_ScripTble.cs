using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Xml.XPath;
using UnityEngine;
[CreateAssetMenu(fileName = "Player_ScripTble",menuName = "SO/Player_ScripTble")]
public class Player_ScripTble : ScriptableObject
{
    public int Level = 1;
    public int Coat = 0;
    public float CurrentHp = 100f;
    public float Dmae = 2f;
    public float MaxHp = 100f;
    public float Amor = 10f;
    public int Xp;
    public float Guard = 10;
    public bool boss1;
    public bool boss2;
    public bool BossKey;
    public int Meat;
    public int HpHeal;

    public virtual void ResetInfo()
    {
        this.Level = 1;
        this.CurrentHp = MaxHp;
        this.Amor = 10f;
    }
}
