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
    private List<ObjectSlot> inventorySlots;
    private List<ObjectSlot> plantSlots;
    private List<ObjectSlot> towerSlots;

    public GameObject prefabInventorySlot;
    public GameObject prefabBuildingSlot;

    // Start is called before the first frame update
    void Start()
    {

        InitPlants();
        InitTower();
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
        inventorySlots = new List<ObjectSlot>();
        for(int i = 0; i < inventory.GetNumberOfResources(); i++)
        {
            GameObject slot = Instantiate(prefabInventorySlot, InventoryPanel.transform);
            ObjectSlot inventorySlot = slot.GetComponent<ObjectSlot>();
            inventorySlot.InitField(inventory.GetItemByItemId(i));
            inventory.OnResourcesChanged += inventorySlot.UpdateItemDisplay;
            
            inventorySlots.Add(inventorySlot);
        }
    }

    private void InitTower()
    {
        TowerPanel = GameObject.Find("TowerPanel");
        towerSlots = new List<ObjectSlot>();
        for(int i = 0; i < 10/*TODO*/; i++)
        {
            GameObject slot = Instantiate(prefabBuildingSlot, TowerPanel.transform);
            ObjectSlot towerSlot = slot.GetComponent<ObjectSlot>();
            towerSlot.InitField(i);
            towerSlots.Add(towerSlot);
        }
    }

    private void InitPlants()
    {
        PlantPanel = GameObject.Find("PlantPanel");
        plantSlots = new List<ObjectSlot>();
        for (int i = 0; i < 10/*TODO*/; i++)
        {
            GameObject slot = Instantiate(prefabBuildingSlot, PlantPanel.transform);
            ObjectSlot plantSlot = slot.GetComponent<ObjectSlot>();
            plantSlot.InitField(i);
            plantSlots.Add(plantSlot);
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
