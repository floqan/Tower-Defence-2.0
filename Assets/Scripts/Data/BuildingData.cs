using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingData : DataObject
{
    public int PathCost { get; set; }

    public GameObject gameObject;

    public int MoneyCost { get; set; }
    // First Value: ItemId
    // Second Value: Amount
    public List<KeyValuePair<int, int>> ResourcesCost;

    public BuildingData(int cost)
    {
        PathCost = cost;
        ResourcesCost = new List<KeyValuePair<int, int>>();
    }

    public BuildingData (string name, string prefabPath){
        Name = name;
        Image = Resources.Load<Sprite>(prefabPath);
        ResourcesCost = new List<KeyValuePair<int, int>>();
    }

    public abstract GameObject CreateGameObject();
}
