using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageController : MonoBehaviour
{
    public Transform storagePanel;

    Inventory inventory;


    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
    }

    public void IncreaseMaxValue(int itemType, int newMaxValue)
    {
        //case -1: MAX_VALUE_MONEY = newMaxValue;
        //    case 1: MAX_VALUE_PLANTS = newMaxValue;
        //    case 2: MAX_VALUE_ELECTRONIC_PARTS = newMaxValue;
        //    case 3: MAX_VALUE_MECHANICAL_PARTS = newMaxValue;
        
        //TODO Check, if update is possible
        inventory.SetMaxValue(itemType, newMaxValue);        
    }

}
