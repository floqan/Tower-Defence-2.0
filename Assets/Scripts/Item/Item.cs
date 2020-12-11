using System.Collections;
using UnityEngine;

//Data Class
public class Item 
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int ItemId { get; set; }
    public int MoneyCost { get; set; }
    public Sprite Image { get; set; }
    public int AmountStored { get; set; }
}
