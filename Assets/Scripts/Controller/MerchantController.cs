using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MerchantController : MonoBehaviour
{
    // public objects
    public GameObject prefabMerchantSlot;
    public GameObject merchantTimer;
    public Transform ItemPanel;
    
    // public variables
    public int RefreshTimeInSeconds;
    
    // private members
    private float refreshTimer;

    // Start is called before the first frame update
    void Start()
    {
        transform.tag = "Merchant";
        refreshTimer = RefreshTimeInSeconds;
        UpdateItems();
    }

    // Update is called once per frame
    void Update()
    {
        refreshTimer -= Time.deltaTime;
        if(refreshTimer < 0)
        {
            refreshTimer = RefreshTimeInSeconds;
            UpdateItems();
        }
        merchantTimer.GetComponentInChildren<TextMeshProUGUI>().text = GetCurrentTime();
    }

    private string GetCurrentTime()
    {
        TimeSpan time = TimeSpan.FromSeconds(refreshTimer);
        return time.ToString("hh':'mm':'ss");
    }

    private void UpdateItems()
    {
        ClearItems();

        GetNewItems().ForEach(delegate (int item)
        {
            GameObject slot = Instantiate(prefabMerchantSlot, ItemPanel);
            slot.GetComponent<Button>().onClick.AddListener(delegate { BuyItem(item);});
            slot.GetComponent<MerchantSlot>().InitSlot(item);
        });
        // TODO Implement update of items that are displayed 
    }

    private List<int> GetNewItems()
    {
        List<int> items = new List<int>();
        //TODO Implement real Method
        items.Add(2);
        items.Add(31);
        return items;
    }

    private void ClearItems()
    {
        foreach (Transform child in ItemPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void BuyItem(int itemId)
    {
        int itemCost = Inventory.instance.GetItemByItemId(itemId).MoneyCost;
        if( Inventory.instance.CheckMoney(itemCost)) 
        {
            Inventory.instance.DecreaseMoney(itemCost);
            Inventory.instance.IncreaseResource(itemId, 1);
        }
    }
}
