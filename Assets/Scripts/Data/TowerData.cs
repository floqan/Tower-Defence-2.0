using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Items", menuName = "Tower")]
public class TowerData : BuildingData
{
    public int AttackDamage;
    public float AttackSpeed;
    public float AttackRadius;
    public int Hitpoints;
    public Dictionary<int, int> UpgradeCost;
    /*
    public struct Tower
    {
        int AttackDamage;
        float AttackSpeed;
        int Hitpoints;
    }

    

    public class GettlingTower : TowerData
    {
        public int AttackDamage = 8;
        public float AttackSpeed = 9;
        public int Hitpoints = 40;
    }

    public class ShockWaveTower : TowerData
    {
        public int AttackDamage = 8;
        public float AttackSpeed = 9;
        public int Hitpoints = 40;
    }

    //TODO ChangeName
    public class PeitschenTower : TowerData
    {
        public int AttackDamage = 8;
        public float AttackSpeed = 9;
        public int Hitpoints = 40;
    }

    public class RocketLauncherTower : TowerData
    {
        public int AttackDamage = 8;
        public float AttackSpeed = 9;
        public int Hitpoints = 40;
    }

    // TODO Change Name
    public class AOETower : TowerData
    {
        public int AttackDamage = 8;
        public float AttackSpeed = 9;
        public int Hitpoints = 40;
    }
    */
}
