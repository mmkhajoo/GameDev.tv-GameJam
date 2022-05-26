using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlot 
{
    float MaxPos { get; }

    float MinPos { get; }

    DirectionType DirectionType { get; }

    void OnRelease(DragItem dragItem, Vector3 dragPosition);
}
