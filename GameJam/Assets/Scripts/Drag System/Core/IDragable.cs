using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDragable
{
    void OnValidPlacement(DirectionType slotTypes);

    void OnInvalidePlacement();
}