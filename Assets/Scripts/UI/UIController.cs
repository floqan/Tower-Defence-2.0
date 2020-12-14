using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIController : MonoBehaviour
{
    public GameObject TowerMenu;
    public GameObject PlantMenu;
    
    private GameObject TowerPanel;
    private GameObject PlantPanel;
    private GameObject InventoryPanel;
    private GameObject MoneyPanel;
    private GameObject MerchantPanel;

    private Inventory inventory;
    private List<InventorySlot> inventorySlots;

    public GameObject prefabInventorySlot;
    
    // Start is called before the first frame update
    void Start()
    {
        
        TowerPanel = GameObject.Find("TowerPanel");
        PlantPanel = GameObject.Find("PlantPanel");
        ShowTowerPanel();
        InitInventory();     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitInventory()
    {
        InventoryPanel = GameObject.Find("InventoryPanel");
        inventory = Inventory.instance;
        inventory.OnMoneyChanged += UpdateMoneyDisplay;
        inventorySlots = new List<InventorySlot>();
        for(int i = 0; i < inventory.GetNumberOfResources(); i++)
        {
            GameObject slot = Instantiate(prefabInventorySlot, InventoryPanel.transform);
            InventorySlot inventorySlot = slot.GetComponent<InventorySlot>();
            inventorySlot.InitField(inventory.GetItemByItemId(i));
            inventory.OnResourcesChanged += inventorySlot.UpdateItemDisplay;
            
            inventorySlots.Add(inventorySlot);
        }
    }
    public void ShowTowerPanel()
    {
        PlantPanel.SetActive(false);
        TowerPanel.SetActive(true);
    }

    public void ShowPlantPanel()
    {
        TowerPanel.SetActive(false);
        PlantPanel.SetActive(true);
    }

    public void UpdateMoneyDisplay()
    {
        MoneyPanel.GetComponent<TextMeshProUGUI>().text = inventory.GetMoney().ToString();
    }

    public void PlaceBuiding(Building building)
    {
        GameManager.instance.CreateBuilding(building);
    }
    internal void OpenTowerMenu()
    {
        throw new NotImplementedException("Open tower menu");
    }

    internal void CloseTowerMenu()
    {
        throw new NotImplementedException("Close tower menu");
    }

    internal void OpenPlantMenu()
    {
        throw new NotImplementedException("Open plant menu");
    }
    internal void ClosePlantMenu()
    {
        throw new NotImplementedException("Close plant menu");
    }
    internal void OpenMerchantMenu()
    {
        MerchantPanel.SetActive(true);
    }
    internal void CloseMerchantMenu()
    {
        MerchantPanel.SetActive(false);
    }
}
