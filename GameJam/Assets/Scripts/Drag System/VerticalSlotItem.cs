using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class VerticalSlotItem : SlotItem
{
    #region Fields

    [SerializeField]
    private DirectionType _directionType;

    [Header("Position Offset")]
    [SerializeField]
    private float _maxPos;
    [SerializeField]
    private float _minPos;

    #endregion

    #region Properties

    public override DirectionType DirectionType => _directionType;

    public override float MaxPos => _maxPos;

    public override float MinPos => _minPos;

    #endregion

    #region Public Methods

    public override void OnRelease(DragItem dragItem, Vector3 dragPosition)
    {

        Vector2 setPosition = Vector2.zero;

        if (dragPosition.y > MaxPos)
            setPosition = new Vector2(transform.position.x, MaxPos);

        else if (dragPosition.y < MinPos)
            setPosition = new Vector2(transform.position.x, MinPos);

        else
            setPosition = new Vector2(transform.position.x, dragPosition.y);

        dragItem.transform.position = Vector2.Lerp(dragItem.transform.position, setPosition, 1f);

        dragItem.OnValidPlacement(DirectionType);
    }

    #endregion
}
