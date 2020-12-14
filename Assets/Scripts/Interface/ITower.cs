using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITower
{
    BuildingData Tower { get; set; }
    float AttackSpeed { get; set; }
    float Radius { get; set; }
    int AttackDamage { get; set; }
    int HitPoints { get; set; }
    Dictionary<int, int> UpgradeCost { get; set; }
    void SetNextTarget();
    void Attack();
    void Upgrade();
}
