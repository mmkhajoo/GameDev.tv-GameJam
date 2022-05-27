using Items;
using Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonItems : Item
{
    [SerializeField]
    private float _distanceDetection;
    [SerializeField]
    private float _detectionSizeMulti;

    private BoxCollider2D _collider;
    private Color _rayColor;
    private Vector2 _directionDetect;


    public override void Initialize()
    {
        _collider = GetComponent<BoxCollider2D>();
        _rayColor = Color.green;
    }

    public override void Execute()
    {
        RaycastHit2D[] hits = Detection();

        foreach (var hit in hits)
        {
            if (hit.transform.TryGetComponent<ObjectController>(out ObjectController objectPosses))
            {
                if (objectPosses.ObjectType == ObjectType.Metal /* && objectPosses.IsEnable */)
                {

                }
            }
        }
    }

    private RaycastHit2D[] Detection()
    {
        _directionDetect = Vector2.zero;

        switch (DirectionType)
        {
            case DirectionType.None:
                break;
            case DirectionType.Up:
                _directionDetect = Vector2.down;
                break;
            case DirectionType.Down:
                _directionDetect = Vector2.up;
                break;
            case DirectionType.Left:
                _directionDetect = Vector2.right;
                break;
            case DirectionType.Right:
                _directionDetect = Vector2.left;
                break;
            default:
                break;
        }
        if (DirectionType == DirectionType.Down || DirectionType == DirectionType.Up)
        {
            Debug.DrawRay(_collider.bounds.center + new Vector3(_collider.bounds.extents.x * _detectionSizeMulti, 0), _directionDetect * (_collider.bounds.extents.y + _distanceDetection), _rayColor);
            Debug.DrawRay(_collider.bounds.center - new Vector3(_collider.bounds.extents.x * _detectionSizeMulti, 0), _directionDetect * (_collider.bounds.extents.y + _distanceDetection), _rayColor);
        }
        else
        {
            Debug.DrawRay(_collider.bounds.center + new Vector3(0, _collider.bounds.extents.y * _detectionSizeMulti), _directionDetect * (_collider.bounds.extents.x + _distanceDetection), _rayColor);
            Debug.DrawRay(_collider.bounds.center - new Vector3(0, _collider.bounds.extents.y * _detectionSizeMulti), _directionDetect * (_collider.bounds.extents.x + _distanceDetection), _rayColor);
        }

        return Physics2D.BoxCastAll(_collider.bounds.center, _collider.bounds.size * _detectionSizeMulti, 0f, _directionDetect, _distanceDetection);
    }
}
