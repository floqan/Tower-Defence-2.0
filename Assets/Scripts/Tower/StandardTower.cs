using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardTower : Building, ITower
{
    public BuildingData Tower { get; set; }
    BuildingData ITower.Tower { get; set; }
    float ITower.AttackSpeed { get; set; }
    float ITower.Radius { get; set; }
    int ITower.AttackDamage { get; set; }
    int ITower.HitPoints { get;  set; }
    Dictionary<int, int> ITower.UpgradeCost { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ITower.Upgrade()
    {
        throw new System.NotImplementedException();
    }
    void ITower.Attack()
    {
        throw new System.NotImplementedException();
    }

    void ITower.SetNextTarget()
    {
        throw new System.NotImplementedException();
    }

}
