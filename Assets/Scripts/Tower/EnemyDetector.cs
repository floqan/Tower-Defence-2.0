using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    private SphereCollider radius;
    private AbstractTower tower;

    private void Awake()
    {
        radius = gameObject.AddComponent<SphereCollider>();
        tower = gameObject.GetComponentInParent<AbstractTower>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            tower.AddEnemyInRange(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            tower.RemoveEnemyInRange(other.transform);
        }
    }

    internal void SetRadius(float attackRadius)
    {
        radius.radius = attackRadius;
    }
}
