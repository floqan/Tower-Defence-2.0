
using System.Collections;
using UnityEngine;

//Data Class
[CreateAssetMenu(fileName = "Items", menuName = "Item")]
public class Item : DataObject
{
    public string Description;
    public int MoneyCost;
    public int AmountStored;
}
