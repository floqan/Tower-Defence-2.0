using System;
using TMPro;
using UnityEngine;

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
        // TODO Implement update of items that are displayed 
    }

    private void ClearItems()
    {
        foreach (Transform child in ItemPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
