using System;
using DefaultNamespace;
using UnityEngine;

public class DashController : MonoBehaviour
{
    private PlayerMovement _playerMovement;

    private BoxCollider2D _boxCollider2D;
    private CircleCollider2D _circleCollider2D;
    private Rigidbody2D _rigidbody2D;

    [SerializeField] private float _dashDistance;
    [SerializeField] private float _dashTime;

    private float _forceValue;
    private Vector3 _targetPosition;

    private void Awake()
    {
        
    }

    private void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
    }

    private void Dash()
    {
        var force = _playerMovement.Direction * _forceValue;

        _rigidbody2D.AddForce(force);

        _boxCollider2D.isTrigger = true;
        _circleCollider2D.isTrigger = true;
    }

    private bool CanDash()
    {
        var targetPosition = _playerMovement.Direction * _dashDistance;

        RaycastHit2D rightHit =
            Physics2D.Raycast(transform.position + _playerMovement.Direction * _boxCollider2D.size.x, Vector2.zero);
        RaycastHit2D leftHit = Physics2D.Raycast(transform.position - _playerMovement.Direction * _boxCollider2D.size.x,
            Vector2.zero);

        if (rightHit.collider != null || leftHit.collider != null)
        {
            return false;
        } 
        
        
        //TODO : Check if Dash Will Out of the Gameplay Area.
        
        

        _targetPosition = targetPosition;
        
        return true;
    }
}