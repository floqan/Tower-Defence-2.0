using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public int ObjectId { get; set; }
    public Image image;
    public TextMeshProUGUI itemDisplay;
    public Image warningYellow;
    public Image warningRed;


    public void InitField(DataObject o)
    {
        if(o.Image != null)
        {
            image.sprite = o.Image;
        }
        ObjectId = o.ObjectId;
        gameObject.GetComponent<Image>().color = MyColors.GetBackgroundColorByObjectType(Inventory.instance.GetItemByItemId(ObjectId).ObjectType);
        UpdateItemDisplay();
    }

    private void UpdateItemDisplay()
    {
        UpdateItemDisplay(ObjectId);
    }

    public void UpdateItemDisplay(int itemId)
    {
        if (itemId == ObjectId)
        {
            int numberOfItems = Inventory.instance.GetValueByItemId(itemId);
            itemDisplay.text = numberOfItems.ToString();
            int maxNumber = Inventory.instance.GetMaxValueByItemId(itemId);
            if (maxNumber == numberOfItems)
            {
                warningYellow.gameObject.SetActive(false);
                warningRed.gameObject.SetActive(true);
            }
            else if(maxNumber * 0.7 < numberOfItems)
            {
                warningRed.gameObject.SetActive(false);
                warningYellow.gameObject.SetActive(true);
            }
            else
            {
                warningRed.gameObject.SetActive(false);
                warningYellow.gameObject.SetActive(false);
            }
        }
    }
}
