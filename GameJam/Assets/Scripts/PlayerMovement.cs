using System;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(CharacterController2D))]
    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController2D controller;

        [SerializeField] private float runSpeed = 40f;

        private float _horizontalMove = 0f;

        private bool _jump = false;
        private void Update()
        {
            _horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            if (Input.GetButtonDown("Jump"))
            {
                _jump = true;
            }
        }

        private void FixedUpdate()
        {
            controller.Move(_horizontalMove * Time.fixedDeltaTime,false,_jump);
            _jump = false;
        }
    }
}