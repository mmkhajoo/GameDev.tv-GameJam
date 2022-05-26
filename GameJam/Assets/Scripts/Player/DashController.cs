using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class DashController : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private GravityController _gravityController;
    private ConstantForce2D _constantForce2D;


    private BoxCollider2D _boxCollider2D;
    private CircleCollider2D _circleCollider2D;
    private Rigidbody2D _rigidbody2D;

    [SerializeField] private float _dashDistance;
    [SerializeField] private float _dashTime;

    private float _forceValue;
    private Vector3 _targetPosition;
    private IEnumerable<DirectionType> _directionTypes;


    private float _counter;
    private bool _dashClicked;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _gravityController = GetComponent<GravityController>();
        _constantForce2D = GetComponent<ConstantForce2D>();


        _boxCollider2D = GetComponent<BoxCollider2D>();
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();


        _directionTypes = Enum.GetValues(typeof(DirectionType)).Cast<DirectionType>();
    }

    private void Start()
    {
        _forceValue = _dashDistance / _dashTime;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Debug Multiplier") /*&& !_dashClicked*/)
        {
            if (CanDash())
            {
                _counter = Time.time;
                Dash();
            }
        }

        if (Time.time - _counter > _dashTime && _dashClicked)
        {
            _dashClicked = false;
            
            _boxCollider2D.isTrigger = false;
            _circleCollider2D.isTrigger = false;

            _constantForce2D.enabled = true;
        }
    }

    private void Dash()
    {
        _constantForce2D.enabled = false;
        
        var force = _playerMovement.Direction * _forceValue;

        Debug.Log(force);
        
        _boxCollider2D.isTrigger = true;
        _circleCollider2D.isTrigger = true;

        _rigidbody2D.velocity = force;
        
        _dashClicked = true;
    }

    private bool CanDash()
    {
        var targetPosition = transform.position + _playerMovement.Direction * _dashDistance;

        Debug.Log(targetPosition);

        RaycastHit2D rightHit =
            Physics2D.Raycast(targetPosition + _playerMovement.Direction * _boxCollider2D.size.x, Vector2.zero);
        RaycastHit2D leftHit = Physics2D.Raycast(targetPosition - _playerMovement.Direction * _boxCollider2D.size.x,
            Vector2.zero);

        if (rightHit.collider != null || leftHit.collider != null)
        {
            return false;
        }


        foreach (var directionType in _directionTypes)
        {
            if (!CheckDirectionWall(directionType, targetPosition))
                return false;
        }

        _targetPosition = targetPosition;

        return true;
    }

    private bool CheckDirectionWall(DirectionType directionType, Vector3 targetPosition)
    {
        switch (directionType)
        {
            case DirectionType.Up:
                if (targetPosition.y > _gravityController.gorundDictionary[directionType].position.y)
                    return false;
                break;
            case DirectionType.Down:
                if (targetPosition.y < _gravityController.gorundDictionary[directionType].position.y)
                    return false;
                break;
            case DirectionType.Left:
                if (targetPosition.x < _gravityController.gorundDictionary[directionType].position.x)
                    return false;
                break;
            case DirectionType.Right:
                if (targetPosition.x > _gravityController.gorundDictionary[directionType].position.x)
                    return false;
                break;
        }

        return true;
    }
}