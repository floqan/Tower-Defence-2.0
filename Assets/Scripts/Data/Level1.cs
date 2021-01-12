using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : LevelData
{
    public Level1()
    {
        
        
        Enemies = new List<KeyValuePair<int, int>>();
        Enemies.Add(new KeyValuePair<int, int>(1, 0));
        //Enemies.Add(new KeyValuePair<int, int>(1, 1));
        //Enemies.Add(new KeyValuePair<int, int>(1, 0));
        //Enemies.Add(new KeyValuePair<int, int>(1, 1));

        Spawns = new List<FieldGridCoordinate> ();
        Spawns.Add(new FieldGridCoordinate(1,2));
        //Spawns.Add(new FieldGridCoordinate(18, 18));

        Goals = new List<FieldGridCoordinate>();
        Goals.Add(new FieldGridCoordinate(14, 15));

        Environment = new List<FieldGridCoordinate>();
        Environment.Add(new FieldGridCoordinate(4, 2));
        Environment.Add(new FieldGridCoordinate(4, 3));
        Environment.Add(new FieldGridCoordinate(4, 4));
        Environment.Add(new FieldGridCoordinate(4, 5));
        Environment.Add(new FieldGridCoordinate(4, 1));
        Environment.Add(new FieldGridCoordinate(4, 6));
        Environment.Add(new FieldGridCoordinate(10, 2));
        Environment.Add(new FieldGridCoordinate(10, 2));
        Environment.Add(new FieldGridCoordinate(10, 1));
        Environment.Add(new FieldGridCoordinate(10, 0));
        Environment.Add(new FieldGridCoordinate(10, 3));
        Environment.Add(new FieldGridCoordinate(10, 4));
        Environment.Add(new FieldGridCoordinate(10, 5));
        Environment.Add(new FieldGridCoordinate(10, 6));
        Environment.Add(new FieldGridCoordinate(10, 7));
        Environment.Add(new FieldGridCoordinate(10, 2));
        Environment.Add(new FieldGridCoordinate(10, 9));
        Environment.Add(new FieldGridCoordinate(11, 9)); //
        Environment.Add(new FieldGridCoordinate(12, 9));
        Environment.Add(new FieldGridCoordinate(12, 8));
        Environment.Add(new FieldGridCoordinate(12, 7));
        Environment.Add(new FieldGridCoordinate(12, 6));
        Environment.Add(new FieldGridCoordinate(12, 5));
        Environment.Add(new FieldGridCoordinate(12, 4));
        Environment.Add(new FieldGridCoordinate(12, 3));
        Environment.Add(new FieldGridCoordinate(12, 2)); 
        Environment.Add(new FieldGridCoordinate(9, 9));
        Environment.Add(new FieldGridCoordinate(8, 9));
        Environment.Add(new FieldGridCoordinate(7, 9));
        Environment.Add(new FieldGridCoordinate(6, 9));
        Environment.Add(new FieldGridCoordinate(5, 9));
        Environment.Add(new FieldGridCoordinate(4, 9));
        Environment.Add(new FieldGridCoordinate(3, 9));
        Environment.Add(new FieldGridCoordinate(2, 9));
        Environment.Add(new FieldGridCoordinate(1, 9));
        Environment.Add(new FieldGridCoordinate(0, 9));
        Environment.Add(new FieldGridCoordinate(20, 14));
        Environment.Add(new FieldGridCoordinate(19, 14));
        Environment.Add(new FieldGridCoordinate(18, 14));
        Environment.Add(new FieldGridCoordinate(17, 14));
        Environment.Add(new FieldGridCoordinate(16, 14));
        Environment.Add(new FieldGridCoordinate(15, 14));
        Environment.Add(new FieldGridCoordinate(14, 14));
        Environment.Add(new FieldGridCoordinate(13, 14));
        Environment.Add(new FieldGridCoordinate(12, 14));
        Environment.Add(new FieldGridCoordinate(11, 14));
        Environment.Add(new FieldGridCoordinate(10, 14));
        Environment.Add(new FieldGridCoordinate(9, 14));






    }
}
