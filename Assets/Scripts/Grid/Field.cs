using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field
{
    private int enemyCounter;
    public bool IsEnvironment { get; set; }
    public bool IsGoal { get; set; }
    
    public FieldGridCoordinate GridCoordinate { get; }
    private float size;
    private Vector3  offset;
    public BuildingData building;


    public Field(int x, int z, float size, Vector3 offset)
    {
        GridCoordinate = new FieldGridCoordinate(x, z);
        this.size = size;
        this.offset = offset;
    }

    public Vector3 GetMiddlePoint()
    {
        return new Vector3(GridCoordinate.X * size + 0.5f * size, 0, GridCoordinate.Z * size + 0.5f * size) + offset; 
    }

    public int GetPathCost()
    {
        return IsEnvironment ? int.MaxValue : (building == null) ? 1 : building.pathCost;
    }

    public FieldGridCoordinate GetFieldGridCoordinate()
    {
        return GridCoordinate;
    }

    public bool IsLocked() => enemyCounter > 0;
    public void LockField() => enemyCounter++;
    public void UnlockField() => enemyCounter--;

    internal FieldGridCoordinate PlaceBuilding(GameObject selection)
    {
        BuildingData buildingData = null;
        if(selection.GetComponent<AbstractTower>() != null)
        {
            buildingData = selection.GetComponent<AbstractTower>().buildingData;
        }
        if (selection.GetComponent<Cropland>() != null)
        {
            buildingData = selection.GetComponent<Cropland>().buildingData;
        }
        else
        {
            if (selection.GetComponent<AbstractPlant>() != null)
            {
                building.building.GetComponent<Cropland>().SetPlant(selection);
            }
        }
        if (buildingData != null)
        {
            building = buildingData;
            return GridCoordinate;
        }
        else
        {
            return null;
        }
    }
}
