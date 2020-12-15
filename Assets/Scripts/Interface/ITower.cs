using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITower : IBuilding
{
    void SetNextTarget();
    void Attack();
    void Upgrade();
}
