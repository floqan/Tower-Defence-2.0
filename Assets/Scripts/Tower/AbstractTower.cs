﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTower : Building<TowerData>, ITower
{
    public EnemyDetector enemyDetector;
    public abstract Transform target { get; set; }
    public bool IsPlacement { get; set; }

    public abstract void Attack();
    public abstract void UpdateTarget();

    protected List<Transform> EnemiesInRange;

    private void Awake()
    {
        buildingData.ObjectType = DataObject.TOWER_TYPE;
        IsPlacement = true;
        gameObject.tag = "Tower";
        buildingData.building = gameObject;
    }

    public void AddEnemyInRange(Transform enemy)
    {
        EnemiesInRange.Add(enemy);
    }

    public void RemoveEnemyInRange(Transform enemy)
    {
        EnemiesInRange.Remove(enemy);
    }
    public void SetColorEnabled()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.clear;
    }

    public void SetColorDisabled()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
}
