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
    private List<BuildingSlot> plantSlots;
    private List<BuildingSlot> towerSlots;

    public GameObject prefabInventorySlot;
    public GameObject prefabBuildingSlot;

    // Start is called before the first frame update
    void Start()
    {
        InitPlants();
        InitTower();
        ShowTowerPanel();
        MoneyPanel = GameObject.Find("MoneyPanel");
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
        UpdateMoneyDisplay();
    }

    private void InitTower()
    {
        TowerPanel = GameObject.Find("TowerPanel");
        towerSlots = new List<BuildingSlot>();
        for(int i = 0; i < GameManager.instance.GetTowerCount(); i++)
        {
            GameObject slot = Instantiate(prefabBuildingSlot, TowerPanel.transform);
            BuildingSlot towerSlot = slot.GetComponent<BuildingSlot>();
            Button slotButton = slot.GetComponent<Button>();
            slotButton.onClick.AddListener(delegate { CreateTower(towerSlot.objectId); });
            towerSlot.InitTower(GameManager.instance.Towers[i].GetComponent<AbstractTower>().buildingData);
            towerSlots.Add(towerSlot);
        }
    }

    private void InitPlants()
    {
        PlantPanel = GameObject.Find("PlantPanel");
        plantSlots = new List<BuildingSlot>();
        for (int i = 0; i < GameManager.instance.GetPlantCount(); i++)
        {
            GameObject slot = Instantiate(prefabBuildingSlot, PlantPanel.transform);
            BuildingSlot plantSlot = slot.GetComponent<BuildingSlot>();
            Button slotButton = slot.GetComponent<Button>();
            slotButton.onClick.AddListener(delegate { CreatePlant(plantSlot.objectId); });
            plantSlot.InitPlant(GameManager.instance.Plants[i].GetComponent<AbstractTower>().buildingData);
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
        MoneyPanel.GetComponentInChildren<TextMeshProUGUI>().text = inventory.GetMoney().ToString();
    }

    void CreateTower(int towerId)
    {
        GameManager.instance.CreateBuilding(towerId, GameManager.TOWER_TYPE);
    }

    void CreatePlant(int plantId)
    {
        GameManager.instance.CreateBuilding(plantId, GameManager.PLANT_TYPE);
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
