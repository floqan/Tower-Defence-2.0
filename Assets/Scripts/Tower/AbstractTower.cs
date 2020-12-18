using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTower : Building<TowerData>, ITower
{
    public abstract void Attack();
    public abstract void SetNextTarget();
    public abstract void Upgrade();
    public abstract GameObject CreateGameObject();

}
