using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Tower", menuName = "Items/Tower")]
public class TowerData : BuildingData
{
    public int AttackDamage;
    public float AttackSpeed;
    public float AttackRadius;
    public float ProjectileSpeed;
    public int Hitpoints;
    public Dictionary<int, int> UpgradeCost;
}
