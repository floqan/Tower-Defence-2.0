using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingData : DataObject
{
    public int pathCost;
    public int moneyCost;
    public GameObject building;

    // First Value: ItemId
    // Second Value: Amount
    [SerializeField]
    public List<Resource> ResourcesCost;// = new List<KeyValuePair<int, int>>(); // TODO Find solution

    [System.Serializable]
    public class Resource
    {
        public int itemId;
        public int itemAmount;
    }
}
