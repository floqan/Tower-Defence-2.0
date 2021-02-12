using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingSlot : MonoBehaviour
{
    public int objectId;
    public Image image;
    public TextMeshProUGUI buildingNameDisplay;

    public void InitImage(DataObject o)
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
    }

    public void InitPlant(DataObject o)
    {
        objectId = o.ObjectId;
        InitImage(o);
        buildingNameDisplay.text = GameManager.instance.GetPlantById(o.ObjectId).Name;
    }
}
