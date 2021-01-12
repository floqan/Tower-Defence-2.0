using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building<T> : MonoBehaviour where T: BuildingData
{
    public T buildingData;
    public bool IsPlacement { get; set; }

}
