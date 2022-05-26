using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SlotItem : MonoBehaviour, ISlot
{
    public abstract float MaxPos { get; }

    public abstract float MinPos { get; }

    public abstract DirectionType DirectionType { get; }

    public abstract void OnRelease(DragItem dragItem, Vector3 dragPosition);
}
