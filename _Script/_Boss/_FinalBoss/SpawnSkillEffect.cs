using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSkillEffect : Spawner
{
    public virtual void GetBySpawn(Transform trans)
    {
        Spawning(ListObj[0],trans.position,trans.rotation);
    }
}
