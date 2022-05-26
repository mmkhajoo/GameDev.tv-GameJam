using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragItem : MonoBehaviour, IDragable
{
    private const float _moveSpeed = 50f;
    private bool _isReturnPosition;
    private Vector3 _returnPosition;
    private DirectionType _returnType;

    private void Start()
    {
        _returnPosition = transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(_returnPosition, transform.position) == 0)
            _isReturnPosition = false;
        

        if (_isReturnPosition)
            transform.position = Vector3.MoveTowards(transform.position, _returnPosition, Time.deltaTime * _moveSpeed);
    }

    public void OnValidPlacement(DirectionType directionType)
    {
        _returnPosition = transform.position;


        if(_returnType != directionType)
        {
            var rotation = transform.rotation;

            switch (directionType)
            {
                case DirectionType.Up:
                    transform.rotation = Quaternion.Euler(0,0,180);
                    _returnType = directionType;
                    break;
                case DirectionType.Down:
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    _returnType = directionType;
                    break;
                case DirectionType.Left:
                    transform.rotation = Quaternion.Euler(0, 0, -90);
                    _returnType = directionType;
                    break;
                case DirectionType.Right:
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                    _returnType = directionType;
                    break;
                default:
                    break;
            }
        }

    }

    public void OnInvalidePlacement()
    {
        _isReturnPosition = true;
    }
}
