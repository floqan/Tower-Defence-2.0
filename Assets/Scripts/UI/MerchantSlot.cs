using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MerchantSlot : MonoBehaviour
{
    public int ItemId { get; set; }
    public Image image;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI cost;

    public void InitSlot(int itemId)
    {
        ItemId = itemId;
        Item item = Inventory.instance.GetItemByItemId(itemId);
        itemName.text = item.Name;
        cost.text = item.MoneyCost.ToString();
        image.sprite = item.Image;
    }    
}
