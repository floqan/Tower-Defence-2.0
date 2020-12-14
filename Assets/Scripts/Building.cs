using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public string Name { get; set; }
    public int PathCost { get; set; }
    public Sprite Preview { get; set; }
    public GameObject prefabBuilding;

    public int MoneyCost;
    // First Value: ItemId
    // Second Value: Amount
    public List<KeyValuePair<int, int>> ResourcesCost;

    public Building()
    {
    }

    public Building(int cost)
    {
        
    }
}
