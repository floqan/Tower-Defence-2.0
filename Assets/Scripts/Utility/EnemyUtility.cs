using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUtility
{
    
    public static GameObject GetEnemyByID(int EnemyId)
    {
        GameObject enemy = Object.Instantiate(Resources.Load<GameObject>("Prefab/Enemy" + EnemyId), Vector3.zero, Quaternion.identity);
        return enemy;
    }
}
