using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUtility
{

    public static void LoadLevel(GridComponent gridComponent, LevelData levelData)
    {
        if (levelData.Goals.Count < 1) throw new MissingComponentException("No Goals set!");
        foreach (FieldGridCoordinate goal in levelData.Goals)
        {
            gridComponent.Grid[goal.X, goal.Z].IsGoal = true;
            GameObject goalObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            goalObject.transform.position = gridComponent.Grid[goal.X, goal.Z].GetMiddlePoint();
            goalObject.GetComponent<Renderer>().material.color = Color.green;
        }
        if(levelData.Spawns.Count < 1) throw new MissingComponentException("No spawn points set!");
        foreach(FieldGridCoordinate spawn in levelData.Spawns)
        {
            gridComponent.Spawns.Add(new KeyValuePair<Field, List<FieldGridCoordinate>>( gridComponent.Grid[spawn.X, spawn.Z], null));
            GameObject spawnObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            spawnObject.transform.position = gridComponent.Grid[spawn.X, spawn.Z].GetMiddlePoint();
            spawnObject.GetComponent<Renderer>().material.color = Color.blue;

        }
        foreach (FieldGridCoordinate environment in levelData.Environment)
        {
            gridComponent.Grid[environment.X, environment.Z].IsEnvironment = true;
            GameObject environmentObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            environmentObject.transform.position = gridComponent.Grid[environment.X, environment.Z].GetMiddlePoint();
            environmentObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    public static LevelData GetLevelData(int level)
    {
        switch (level)
        {
            case 1: return new Level1();
            default: return null;
        }
    }
}


