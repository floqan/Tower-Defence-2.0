﻿using System.Collections;
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
    public List<KeyValuePair<int, int>> ResourcesCost = new List<KeyValuePair<int, int>>(); //Find solution
}
