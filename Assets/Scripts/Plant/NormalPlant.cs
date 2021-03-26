using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPlant : AbstractPlant
{ 
    private void Awake()
    {
        buildingData.ObjectType = DataObject.PLANT_TYPE;
        IsPlacement = true;
        gameObject.tag = "Plant";
        gameObject.layer = LayerMask.NameToLayer("Plant");
        buildingData.building = gameObject;
    }

    public override GameObject CreateGameObject()
    {
        throw new System.NotImplementedException();
    }

    public override void Harvest()
    {
        if (currentState != MAX_STATE)
        {
            return;
        }
        //Play Animation
        Inventory.instance.IncreaseResource(buildingData.ObjectId, buildingData.YieldsPerHarvest);
        
        Destroy(gameObject);
    }
    public override void Grow()
    {
        if (currentTime < buildingData.GrowTimePerStep && currentState < MAX_STATE)
        {
            currentTime += Time.deltaTime;
            if (currentTime > buildingData.GrowTimePerStep)
            {
                Destroy(Model);
                switch (currentState)
                {
                    case (1):
                        Model = Instantiate(buildingData.Step2, gameObject.transform);
                        currentState = 2;
                        currentTime = 0;
                        break;
                    case (2):
                        Model = Instantiate(buildingData.Step3, gameObject.transform);
                        currentState = 3;
                        currentTime = 0;
                        break;
                    case (MAX_STATE):/*
                        Add some Feedback. Maybe sparkles or some sign
                        */
                        break;
                }
            }
        }
    }
}
