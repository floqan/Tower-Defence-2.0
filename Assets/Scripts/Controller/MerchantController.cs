using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantController : MonoBehaviour
{
    public GameObject prefabMerchantSlot;

    private Transform MerchantPanel;
    // Start is called before the first frame update
    void Start()
    {
        transform.tag = "Merchant";
        MerchantPanel = GameObject.Find("MerchantPanel").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
