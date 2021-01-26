using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlant : IBuilding
{
    void Harvest();
    void Grow() ;
}
