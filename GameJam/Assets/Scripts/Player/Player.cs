using System;
using Objects;
using UnityEngine;

namespace DefaultNamespace
{
    public class Player : MonoBehaviour , IPlayer
    {
        #region Fields

        private PlayerMovement _playerMovement;

        private IObject _objectTransitionedTo;
        
        private PlayerStateType _currentPlayerStateType = PlayerStateType.None;

        #endregion

        #region Events
        public event Action<PlayerStateType> OnPlayerStateChanged;
        public event Action OnPlayerJumped;
        
        #endregion

        #region Private Properties

        private bool isPlayerMoving => _playerMovement.VerticalMove != 0f || _playerMovement.HorizontalMove != 0f;

        #endregion
        
        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();

            _playerMovement.OnJump += () => { OnPlayerJumped?.Invoke(); };
        }

        private void Update()
        {
            if (isPlayerMoving)
            {
                SetPlayerState(PlayerStateType.Walking);
            }

            if (!isPlayerMoving && _playerMovement.IsGrounded && _currentPlayerStateType != PlayerStateType.Idle)
            {
                SetPlayerState(PlayerStateType.Idle);
            }
        }


        private void SetPlayerState(PlayerStateType playerStateType)
        {
            if(playerStateType == _currentPlayerStateType)
                return;

            _currentPlayerStateType = playerStateType;
            OnPlayerStateChanged?.Invoke(_currentPlayerStateType);
        }

        public void Enable()
        {
            _playerMovement.enabled = true;
        }

        public void Disable()
        {
            _playerMovement.enabled = false;
        }

        public void Transition(ObjectController transitableObject)
        {
            _objectTransitionedTo = transitableObject;

            //TODO : Set Item Function For Transition.
            //TODO : Set Player Game Object Child of The Item;
        }

        public void GetOutFromItem()
        {
            Enable();
            
            //TODO : Set Player Position Base On where Object was
        }

        public void Die()
        {
            //TODO : Create Event and and fire it when player die.
        }
        
        //TODO : Search for Item Around Player for Transition in Update Method.
    }
}