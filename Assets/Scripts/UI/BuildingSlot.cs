using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class BuildingSlot : MonoBehaviour
{
    public int objectId { get; set; }
    public Image image;
    public TextMeshProUGUI buildingNameDisplay;
    public Transform RequirementsPanel;

    public GameObject RequirmentSlotPrefab;

    private void InitImage(DataObject o)
    {
        if (o.Image != null)
        {
            image.sprite = o.Image;
        }
    }

    public void InitTower(DataObject o)
    {
        objectId = o.ObjectId;
        InitImage(o);
        buildingNameDisplay.text = GameManager.instance.GetTowerById(o.ObjectId).Name;
        InitRequirementsTowers();
    }

    public void InitPlant(DataObject o)
    {
        objectId = o.ObjectId;
        InitImage(o);
        buildingNameDisplay.text = GameManager.instance.GetPlantById(o.ObjectId).Name;
        InitRequirementsPlants();
    }

    private void InitRequirementsPlants()
    {
        int money = GameManager.instance.GetPlantById(objectId).moneyCost;
        if(money > 0)
        {
            GameObject slot = Instantiate(RequirmentSlotPrefab, RequirementsPanel);
            slot.GetComponentInChildren<TextMeshProUGUI>().text = money.ToString();
            slot.GetComponentInChildren<Image>().sprite = Inventory.instance.GetItemByItemId(0).Image;
        }
        
        foreach(KeyValuePair<int,int> pair in GameManager.instance.GetPlantById(objectId).ResourcesCost)
        {
            GameObject slot = Instantiate(RequirmentSlotPrefab, RequirementsPanel);
            slot.GetComponentInChildren<TextMeshProUGUI>().text = pair.Value.ToString();
            slot.GetComponentInChildren<Image>().sprite = Inventory.instance.GetItemByItemId(pair.Key).Image;
        }

    }
    private void InitRequirementsTowers()
    {
        int money = GameManager.instance.GetTowerById(objectId).moneyCost;
        if (money > 0)
        {
            GameObject slot = Instantiate(RequirmentSlotPrefab, RequirementsPanel);
            slot.GetComponentInChildren<TextMeshProUGUI>().text = money.ToString();
            slot.GetComponentInChildren<Image>().sprite = Inventory.instance.GetItemByItemId(0).Image;
        }

        foreach (KeyValuePair<int, int> pair in GameManager.instance.GetTowerById(objectId).ResourcesCost)
        {
            GameObject slot = Instantiate(RequirmentSlotPrefab, RequirementsPanel);
            slot.GetComponentInChildren<TextMeshProUGUI>().text = pair.Value.ToString();
            slot.GetComponentInChildren<Image>().sprite = Inventory.instance.GetItemByItemId(pair.Key).Image;
        }

    }
}
