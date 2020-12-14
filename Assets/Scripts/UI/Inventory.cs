
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    
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
    }
    #endregion

    public event Action<int> OnResourcesChanged;
    public event Action OnMoneyChanged;

    private Dictionary<int, Item> resources;
    private int Money;
    public int MaxValue { get; set; }

    public int GetNumberOfResources()
    {
        return resources.Count;
    }
    public void IncreaseResource(int itemId, int value)
    {
        resources[itemId].AmountStored += value;
        if(resources[itemId].AmountStored > MaxValue)
        {
            resources[itemId].AmountStored = MaxValue;
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
