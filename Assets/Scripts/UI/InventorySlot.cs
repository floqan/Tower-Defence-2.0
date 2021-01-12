using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public int objectId;
    public Image image;
    public TextMeshProUGUI itemDisplay;

    public void InitField(DataObject o)
    {
        if(o.Image != null)
        {
            image.sprite = o.Image;
        }
        objectId = o.ObjectId;
        UpdateItemDisplay();
    }

    private void UpdateItemDisplay()
    {
        UpdateItemDisplay(objectId);
    }

    public void UpdateItemDisplay(int itemId)
    {
            if (itemId == objectId)
            {
                itemDisplay.text = Inventory.instance.GetValueByItemId(itemId).ToString();
            }
    }
}
