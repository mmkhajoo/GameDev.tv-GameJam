using System;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(CharacterController2D))]
    public class PlayerMovement : MonoBehaviour
    {

        public event Action OnJump;

        public float VerticalMove => _verticalMove;
        public float HorizontalMove => _horizontalMove;
        public bool IsGrounded => controller.IsGrounded;
        
        
        public CharacterController2D controller;

        [SerializeField] private float runSpeed = 40f;

        private float _verticalMove = 0f;
        private float _horizontalMove = 0f;

        private bool _jump = false;

        private ConstantForce2D _constantForce2D;
        
        //TODO : Add Events for Play Idle and Move Animation on Player

        private void Awake()
        {
            _constantForce2D = GetComponent<ConstantForce2D>();
            
            controller.OnJumpAvailable += JumpAvailable;
        }

        private void JumpAvailable()
        {
            _jump = false;
        }

        private void Update()
        {
            _verticalMove = Input.GetAxisRaw("Vertical") * runSpeed;
            _horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            Debug.DrawLine(transform.position, transform.position + transform.right * 10, Color.yellow);

            if (Input.GetButtonDown("Jump") && !_jump)
            {
                _jump = true;
                OnJump?.Invoke();
            }
        }

        private void FixedUpdate()
        {
            if (_constantForce2D.force.y != 0f)
            {
                _verticalMove = 0;
            }
            else if(_constantForce2D.force.x != 0f)
            {
                _horizontalMove = 0;
            }
            
            controller.Move(_verticalMove,_horizontalMove * Time.fixedDeltaTime,false,_jump);
        }
    }
}