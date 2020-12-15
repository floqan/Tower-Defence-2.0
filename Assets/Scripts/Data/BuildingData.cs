using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingData : DataObject
{
    public int pathCost;
    public GameObject gameObject;
    public int moneyCost;
    
    // First Value: ItemId
    // Second Value: Amount
    public List<KeyValuePair<int, int>> ResourcesCost;

}
