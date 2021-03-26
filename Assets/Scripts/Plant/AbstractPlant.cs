using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPlant : Building<PlantData>, IPlant
{
    protected const int MAX_STATE = 3;

    protected int currentState;
    protected float currentTime;
    public GameObject Model;
    public bool IsPlacement { get; set; }
    public Cropland Cropland { get; set; }

    private void Start()
    {
        if (buildingData.Step1 != null)
        {
            Model = Instantiate(buildingData.Step1, gameObject.transform);
        }
        currentState = 1;
        currentTime = 0;
        gameObject.layer = LayerMask.NameToLayer("Plant");
        buildingData.currentHitPoints = buildingData.maxHitPoints;
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

    public abstract void Harvest();

    public abstract GameObject CreateGameObject();

    public virtual void SetColorEnabled()
    {
        Model.GetComponent<Renderer>().material.color = Color.green;
    }

    public virtual void SetColorDisabled()
    {
        Model.GetComponent<Renderer>().material.color = Color.red;
    }

    public void OnDamage(int damage)
    {
        buildingData.currentHitPoints -= damage;
        throw new System.NotImplementedException();
    }
}
