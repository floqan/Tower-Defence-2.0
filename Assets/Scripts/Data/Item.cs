
using System.Collections;
using UnityEngine;

//Data Class
[CreateAssetMenu(fileName = "Item", menuName = "Items/Item")]
public class Item : DataObject
{
    public string Description;
    public int MoneyCost;
    public int dropChance;
    public GameObject itemModel;
    public int AmountStored { get; set; }
}
