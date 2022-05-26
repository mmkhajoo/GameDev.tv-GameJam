using System;
using Objects;
using UnityEngine;

namespace DefaultNamespace
{
    public class Player : MonoBehaviour , IPlayer
    {

        [Header("Transition Configs")]
        
        [SerializeField]private float _searchObjectRadius;
        
        
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

            if (Input.GetKeyDown(KeyCode.E) && _objectTransitionedTo != null)
            {
                CheckTransitionObjects();
            }
            else
            {
                //TODO : Get Out From Object.
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
        
        private void CheckTransitionObjects()
        {
            
            float minDistance = Single.MaxValue;

            ObjectController selectedObjectController = null;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _searchObjectRadius);
            for (int i = 0; i < colliders.Length; i++)
            {
                var currentGameObject = colliders[i].gameObject;
                
                if (currentGameObject != gameObject)
                {
                   if(currentGameObject.TryGetComponent(out ObjectController objectController))
                   {
                       var distance = Vector3.Distance(transform.position, objectController.transform.position);
                       
                       if (distance < minDistance)
                       {
                           selectedObjectController = objectController;
                           minDistance = distance;
                       }
                   }
                }
            }

            if (selectedObjectController != null)
            {
                Transition(selectedObjectController);
            }
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