using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public int itemId;
    public Image image;
    public TextMeshProUGUI itemDisplay;

    public void InitField(Item item)
    {
        if(item.Image != null)
        {
            image.sprite = item.Image;
        }
        itemId = item.ItemId;
        UpdateItemDisplay();
    }

    private void UpdateItemDisplay()
    {
        UpdateItemDisplay(itemId);
    }

    public void UpdateItemDisplay(int itemId)
    {
        if (itemId == this.itemId)
        {
            itemDisplay.text = Inventory.instance.GetValueByItemId(itemId).ToString();
        }
    }
}
