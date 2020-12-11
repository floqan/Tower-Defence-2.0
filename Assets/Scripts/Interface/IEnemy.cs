using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    void RecalculatePath(object sender, CoordinateEventArgs args);
}
