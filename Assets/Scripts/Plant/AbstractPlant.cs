using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPlant : Building<PlantData>, IPlant
{
    protected int currentState;
    protected float currentTime;
    public GameObject Model;
    public bool IsPlacement { get; set; }

    private void Start()
    {
        if (buildingData.Step1 != null)
        {
            Model = Instantiate(buildingData.Step1, gameObject.transform);
        }
        currentState = 1;
        currentTime = 0;
    }

    private void Update()
    {
        if (IsPlacement)
        {
            return;
        }
        Grow();
    }

    public abstract void Grow();

    public void Harvest()
    {
        if(currentState != 4)
        {
            return;
        }

    }
    public abstract GameObject CreateGameObject();

    public virtual void SetColorEnabled()
    {
        Model.GetComponent<Renderer>().material.color = Color.clear;
    }

    public virtual void SetColorDisabled()
    {
        Model.GetComponent<Renderer>().material.color = Color.red;
    }
}
