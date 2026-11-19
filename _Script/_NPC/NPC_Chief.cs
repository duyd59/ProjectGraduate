using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Chief : MyObj
{
    protected virtual void OnTriggerEnter2D(Collider2D coll)
    {
        if (!(coll.gameObject.CompareTag("Player"))) return;
        Debug.Log("오셨습니까, 왕자님. 기다리고 있었습니다. 마을의 미래가 왕자님의 손에 달려있습니다. 비밀 수련 구역으로 가는 열쇠입니다. 부디 몸조심하십시오.");
    }
}
