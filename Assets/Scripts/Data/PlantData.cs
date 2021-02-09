using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Plant", menuName = "Items/Plant")]
public class PlantData : BuildingData
{
    public int GrowTimePerStep;
    public int YieldsPerHarvest;

    public GameObject Step1;
    public GameObject Step2;
    public GameObject Step3;
    public GameObject Step4;
}
