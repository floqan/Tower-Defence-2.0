using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelData 
{
    // First Value: EnemyId 
    // Second Value: Spawnpoint
    public List<KeyValuePair<int, int>> Enemies { get; set; }

    public List<FieldGridCoordinate> Spawns { get; set; }

    public List<FieldGridCoordinate> Goals { get; set; }

    public List<FieldGridCoordinate> Environment { get; set; }

    internal int GetNumberOfEnemies()
    {
        return Enemies.Count;
    }
}
