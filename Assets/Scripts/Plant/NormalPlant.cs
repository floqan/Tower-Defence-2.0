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
    }

    public override GameObject CreateGameObject()
    {
        throw new System.NotImplementedException();
    }
    public override void Grow()
    {
        if (currentTime < buildingData.GrowTimePerStep && currentState < 4)
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
                        break;
                    case (2):
                        Model = Instantiate(buildingData.Step3, gameObject.transform);
                        currentState = 3;
                        break;
                    case (3):
                        Model = Instantiate(buildingData.Step4, gameObject.transform);
                        currentState = 4;
                        break;
                }
            }
        }
    }
}
