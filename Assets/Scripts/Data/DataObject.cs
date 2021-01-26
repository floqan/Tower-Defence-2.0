using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DataObject : ScriptableObject
{
    public const int PLANT_TYPE = 1;
    public const int CROPLAND_TYPE = 2;
    public const int TOWER_TYPE = 3;

    public int ObjectId;
    public Sprite Image;
    public string Name;
    public int ObjectType;

}
