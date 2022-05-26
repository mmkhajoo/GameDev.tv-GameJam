using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlot 
{
    float MaxPos { get; }

    float MinPos { get; }

    DirectionType DirectionType { get; }

    void SetScrollSlot(bool isScroll);

    void OnPlaceDragItem(DragItem dragItem, Vector3 dragPosition);

    Vector3 GetCurrentPositionInSlot(Vector3 inputPosition);
}
