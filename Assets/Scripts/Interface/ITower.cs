using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITower : IBuilding
{
    void UpdateTarget();
    void Attack();
}
