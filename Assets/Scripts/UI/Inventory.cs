using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int MAX_VALUE_PLANTS = 50; //1
    private int MAX_VALUE_ELECTRONIC_PARTS = 20; //2
    private int MAX_VALUE_MECHANICAL_PARTS = 20; //3
    #region Singleton
    public static Inventory instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Achtung, es wurden mehrere Inventare erstellt");
        }
        resources = new Dictionary<int, Item>();
        foreach (Item item in items)
        {
            if (resources.ContainsKey(item.ObjectId))
            {
                throw new ItemException("Item with itemId " + item.ObjectId + " was added twice to the Inventory");
            }
            resources.Add(item.ObjectId, item);
        }
    }
    #endregion

    public event Action<int> OnResourcesChanged;
    public event Action OnMoneyChanged;

    public List<Item> items;
    
    private Dictionary<int, Item> resources;
    private int Money;
    public int MaxValue;

    public int GetNumberOfResources()
    {
        return resources.Count;
    }

    public int GetMaxValueByItemId(int itemId)
    {
        try {
            return GetMaxValueByObjectId(GetItemByItemId(itemId).ObjectType);
        }
        catch(ItemException)
        {
            throw new ItemException("No ObjectType " + GetItemByItemId(itemId).ObjectType + " for Item with itemId " + itemId + " found." );
        }
    }

    public int GetMaxValueByObjectId(int objectId)
    {
        switch (objectId)
        {
            case 1:
                return MAX_VALUE_PLANTS;
            case 2:
                return MAX_VALUE_ELECTRONIC_PARTS;
            case 3:
                return MAX_VALUE_MECHANICAL_PARTS;
            default: throw new ItemException("No ObjectType for objectId " + objectId + " found.");
        }
    }
    public List<int> GetIndexes() => resources.Keys.ToList();
    public void IncreaseResource(int itemId, int value)
    {
        if (!resources.ContainsKey(itemId))
        {
            throw new KeyNotFoundException("Can not find ItemKey " + itemId + " in Inventory Dictionary");
            //Debug.LogError("Can not find ItemKey " + itemId + " in Inventory Dictionary");
            //return;
        }
        resources[itemId].AmountStored += value;
        if(resources[itemId].AmountStored > GetMaxValueByItemId(itemId))
        {
            //TODO Add MaxValueReached Warning / Maybe highlight the Max Amount of this objectType
            resources[itemId].AmountStored = GetMaxValueByItemId(itemId);
        }
        OnResourcesChanged(itemId);
    }

    public bool DecreaseResource(int itemId, int value)
    {
        if (resources[itemId].AmountStored >= value)
        {
            resources[itemId].AmountStored -= value;
            OnResourcesChanged(itemId);
            return true;
        }
        return false;
    }

    public void SetMaxValue(int objectId, int newMaxValue)
    {
        switch (objectId)
        {
            case 1: MAX_VALUE_PLANTS = newMaxValue;
                break;
            case 2: MAX_VALUE_ELECTRONIC_PARTS = newMaxValue;
                break;
            default: throw new ItemException("No objectType for id " + objectId + " found");
        }
    }
    public void IncreaseMoney(int value)
    {
        Money += value;
        if(Money > 999)
        {
            Money = 999;
        }
        OnMoneyChanged();
    }

    public bool DecreaseMone(int value)
    {
        if(Money >= value)
        {
            Money -= value;
            OnMoneyChanged();
            return true;
        }
        return false;
    }

    public int GetValueByItemId(int itemId)
    {
        return resources[itemId].AmountStored;
    }

    public Item GetItemByItemId(int itemId)
    {
        return resources[itemId];
    }

    public int GetMoney()
    {
        return Money;
    }
}
