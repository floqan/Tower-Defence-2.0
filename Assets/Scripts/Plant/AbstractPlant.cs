using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPlant : Building<PlantData>, IPlant
{
    public abstract void Grow();
    public abstract void Harvest();
    public abstract GameObject CreateGameObject();
}
