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
    private GameObject MaxValuePlantsPanel;
    private GameObject MaxValueElectronicPartsPanel;
    private GameObject MaxValueMechanicPartsPanel;
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
        MerchantPanel = GameObject.Find("MerchantPanel");
        InitInventory();
        CloseMerchantMenu();
    }

    private void InitInventory()
    {
        InventoryPanel = GameObject.Find("InventoryPanel");
        inventory = Inventory.instance;
        inventory.OnMoneyChanged += UpdateMoneyDisplay;
        inventorySlots = new List<InventorySlot>();
        List<int> indexes = inventory.GetIndexes();
        for (int i = 0; i < inventory.GetNumberOfResources(); i++)
        {
            if (inventory.GetItemByItemId(indexes[i]).ObjectId == 0) continue; //Dont instantiate inventory slot for money

            GameObject slot = Instantiate(prefabInventorySlot, InventoryPanel.transform);
            InventorySlot inventorySlot = slot.GetComponent<InventorySlot>();
            inventorySlot.InitField(inventory.GetItemByItemId(indexes[i]));
            inventory.OnResourcesChanged += inventorySlot.UpdateItemDisplay;
            inventorySlots.Add(inventorySlot);
        }

        MoneyPanel = GameObject.Find("MoneyPanel");

        MaxValuePlantsPanel = GameObject.Find("MaxPlantsPanel");
        MaxValuePlantsPanel.GetComponent<Image>().color = MyColors.Background_UI_Plants;
        MaxValueElectronicPartsPanel = GameObject.Find("MaxElectronicPartsPanel");
        MaxValueElectronicPartsPanel.GetComponent<Image>().color = MyColors.Background_UI_Electronic_Parts;
        MaxValueMechanicPartsPanel = GameObject.Find("MaxMechanicPartsPanel");
        MaxValueMechanicPartsPanel.GetComponent<Image>().color = MyColors.Background_UI_Mechanic_Parts;

        UpdateDisplays();
    }

    private void UpdateDisplays()
    {
        UpdateMoneyDisplay();
        UpdateMaxPlantsDisplay();
        UpdateMaxElectronicPartsDisplay();
        UpdateMaxMechanicPartsDisplay();
    }

    private void InitTower()
    {
        TowerPanel = GameObject.Find("TowerPanel");
        towerSlots = new List<BuildingSlot>();
        for (int i = 0; i < GameManager.instance.GetTowerCount(); i++)
        {
            GameObject slot = Instantiate(prefabBuildingSlot, TowerPanel.transform);
            BuildingSlot towerSlot = slot.GetComponent<BuildingSlot>();
            towerSlot.InitTower(GameManager.instance.Towers[i].GetComponent<AbstractTower>().buildingData);
            towerSlots.Add(towerSlot);

            Button slotButton = slot.GetComponent<Button>();
            slotButton.onClick.AddListener(delegate { CreateTower(towerSlot.objectId); });
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
            plantSlot.InitPlant(GameManager.instance.Plants[i].GetComponent<AbstractPlant>().buildingData);
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
    public void UpdateMaxPlantsDisplay()
    {
        MaxValuePlantsPanel.GetComponentInChildren<TextMeshProUGUI>().text = inventory.GetMaxValueByObjectId(1).ToString();
    }
    public void UpdateMaxElectronicPartsDisplay()
    {
        MaxValueElectronicPartsPanel.GetComponentInChildren<TextMeshProUGUI>().text = inventory.GetMaxValueByObjectId(2).ToString();
    }
    public void UpdateMaxMechanicPartsDisplay()
    {
        MaxValueMechanicPartsPanel.GetComponentInChildren<TextMeshProUGUI>().text = inventory.GetMaxValueByObjectId(3).ToString();

    }
    void CreateTower(int towerId)
    {
        if (GameManager.instance.CheckResources(towerId))
        {
            GameManager.instance.CreateBuilding(towerId, GameManager.TOWER_TYPE);
        }
        else
        {
            // TODO Show warning that ressources are to low
        }
    }

    void CreatePlant(int plantId)
    {
        if (GameManager.instance.CheckResources(plantId))
        {
            GameManager.instance.CreateBuilding(plantId, GameManager.PLANT_TYPE);
        }
        else
        {
            // TODO Show warning that ressources are to low
        }
    }
    
    internal void OpenTowerMenu()
    {
        throw new NotImplementedException("Open tower menu");
    }

    internal void CloseTowerMenu()
    {
        if(TowerMenu != null)
        {
            TowerMenu.SetActive(false);
        }
        throw new NotImplementedException("Close tower menu");
    }

    internal void OpenPlantMenu()
    {
        throw new NotImplementedException("Open plant menu");
    }
    internal void ClosePlantMenu()
    {
        if(PlantMenu != null)
        {
            PlantMenu.SetActive(false);
        }
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
