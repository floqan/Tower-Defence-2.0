using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingData : DataObject
{
    public int pathCost;
    public int moneyCost;
    public GameObject building;

    public int maxHitPoints;
    public int currentHitPoints;

    [SerializeField]
    public List<Resource> ResourcesCost;

    [System.Serializable]
    public class Resource
    {
        public int itemId;
        public int itemAmount;
    }
}
