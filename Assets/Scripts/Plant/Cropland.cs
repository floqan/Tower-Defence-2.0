using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cropland : AbstractPlant
{
    public AbstractPlant plant { get; set; }

    private void Awake()
    {
        buildingData.ObjectType = DataObject.CROPLAND_TYPE;
        IsPlacement = false;
        gameObject.tag = "Cropland";
    }

    public override GameObject CreateGameObject()
    {
        throw new System.NotImplementedException();
    }

    public override void Grow()
    {

    }
    public override void SetColorEnabled()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.clear;
    }

    public override void SetColorDisabled()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
}
