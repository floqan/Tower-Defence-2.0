using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuilding
{
    bool IsPlacement { get; set; }

    void SetColorEnabled();

    void SetColorDisabled();
    void OnDamage(int damage);
}
