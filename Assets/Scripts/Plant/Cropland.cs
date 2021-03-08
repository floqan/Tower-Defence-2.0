using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cropland : AbstractPlant
{
    public AbstractPlant Plant { get; set; }

    private void Awake()
    {
        buildingData.ObjectType = DataObject.CROPLAND_TYPE;
        IsPlacement = false;
        gameObject.tag = "Cropland";
        buildingData.building = gameObject;
    }

    public override GameObject CreateGameObject()
    {
        throw new System.NotImplementedException();
    }

    public override void Grow(){}
    public override void Harvest()
    {
        if(Plant != null)
        {
            Plant.GetComponent<NormalPlant>().Harvest();
        }
    }
    public override void SetColorEnabled()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }

    public override void SetColorDisabled()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    public void SetPlant(GameObject plantObject)
    {
        Plant = plantObject.GetComponent<AbstractPlant>();
        plantObject.transform.SetParent(gameObject.transform);
        plantObject.GetComponent<AbstractPlant>().Cropland = this;

    }
}
