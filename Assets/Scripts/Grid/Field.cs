﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field 
{
    private int enemyCounter;
    public bool IsEnvironment { get; set; }
    public bool IsGoal { get; set; }
    
    public FieldGridCoordinate gridCoordinate { get; }
    private float size;
    private Vector3  offset;
    public BuildingData building;


    public Field(int x, int z, float size, Vector3 offset)
    {
        gridCoordinate = new FieldGridCoordinate(x, z);
        this.size = size;
        this.offset = offset;
    }

    public Vector3 GetMiddlePoint()
    {
        return new Vector3(gridCoordinate.X * size + 0.5f * size, 0, gridCoordinate.Z * size + 0.5f * size) + offset; 
    }

    public int GetPathCost()
    {
        return IsEnvironment ? int.MaxValue : (building == null) ? 1 : building. pathCost;
    }

    public FieldGridCoordinate GetFieldGridCoordinate()
    {
        return gridCoordinate;
    }

    public bool IsLocked() => enemyCounter > 0;
    public void LockField() => enemyCounter++;
    public void UnlockField() => enemyCounter--;

    internal FieldGridCoordinate PlaceBuilding(GameObject selection)
    {
        if(selection.GetComponent<AbstractTower>() != null)
        {
            building = selection.GetComponent<AbstractTower>().buildingData;
            return gridCoordinate;
        }
        if(selection.GetComponent<AbstractPlant>() != null)
        {
            building = selection.GetComponent<AbstractPlant>().buildingData;
            return gridCoordinate;
        }
        return null;
    }
}
