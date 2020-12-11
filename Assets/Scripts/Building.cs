using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building
{
    public string Name { get; set; }
    public int PathCost { get; set; }
    public Sprite Preview { get; set; }
    public GameObject gameObject;

    public int MoneyCost { get; set; }
    // First Value: ItemId
    // Second Value: Amount
    public List<KeyValuePair<int, int>> ResourcesCost;

    public Building(int cost)
    {
        PathCost = cost;
        ResourcesCost = new List<KeyValuePair<int, int>>();
    }

    public Building (string name, string prefabPath){
        Name = name;
        Preview = Resources.Load<Sprite>(prefabPath);
        ResourcesCost = new List<KeyValuePair<int, int>>();
    }

    public abstract GameObject CreateGameObject();
}
