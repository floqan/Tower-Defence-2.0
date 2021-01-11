using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTower : Building<TowerData>, ITower
{
    public abstract Transform target { get; set; }
    public abstract void Attack();
    public abstract void UpdateTarget();

}
