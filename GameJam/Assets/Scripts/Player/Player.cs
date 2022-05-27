using System;
using Managers;
using Objects;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class Player : MonoBehaviour, IPlayer
    {
        [Header("Transition Configs")] [SerializeField]
        private float _searchObjectRadius = 1f;

        [SerializeField] private float _transitionTime = 0.5f;

        #region Fields

        private ObjectController _objectTransitionedTo;

        private PlayerStateType _currentPlayerStateType = PlayerStateType.None;

        #endregion

        #region Controllers

        private PlayerMovement _playerMovement;
        private CharacterController2D _characterController2D;
        private GravityController _gravityController;
        private DashController _dashController;
        private ConstantForce2D _constantForce2D;
        private BoxCollider2D _boxCollider2D;
        private CircleCollider2D _circleCollider2D;
        private Rigidbody2D _rigidbody2D;

        #endregion

        #region Events

        [Header("Events")] [SerializeField] private PlayerStateEvent _onPlayerStateChanged;
        [SerializeField] private UnityEvent OnPlayerLand;
        [SerializeField] private UnityEvent OnPlayerJumped;
        [SerializeField] private UnityEvent OnTransitioned;
        [SerializeField] private UnityEvent OnPlayerGotOut;

        #endregion

        #region Private Properties

        private bool isPlayerMoving => _playerMovement.VerticalMove != 0f || _playerMovement.HorizontalMove != 0f;

        #endregion

        private bool _isTransitioning;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _characterController2D = GetComponent<CharacterController2D>();
            _gravityController = GetComponent<GravityController>();
            _dashController = GetComponent<DashController>();
            _constantForce2D = GetComponent<ConstantForce2D>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _circleCollider2D = GetComponent<CircleCollider2D>();
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _playerMovement.OnJump += () => { OnPlayerJumped?.Invoke(); };
            _playerMovement.OnLand += () => { OnPlayerLand?.Invoke(); };
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

            if (Input.GetKeyDown(KeyCode.E) && !_isTransitioning)
            {
                if (_objectTransitionedTo == null)
                {
                    CheckTransitionObjects();
                }
                else
                {
                    GetOutFromItem();
                }
            }
        }

        private void SetPlayerState(PlayerStateType playerStateType)
        {
            if (playerStateType == _currentPlayerStateType)
                return;

            _currentPlayerStateType = playerStateType;
            _onPlayerStateChanged?.Invoke(_currentPlayerStateType);
        }

        public void Enable()
        {
            _playerMovement.enabled = true;
            _characterController2D.enabled = true;
            _boxCollider2D.isTrigger = false;
            _circleCollider2D.isTrigger = false;
            _constantForce2D.enabled = true;
            _gravityController.enabled = true;
            _dashController.enabled = true;
        }

        public void Disable()
        {
            _rigidbody2D.velocity = Vector2.zero;

            _playerMovement.enabled = false;
            _characterController2D.enabled = false;
            _boxCollider2D.isTrigger = true;
            _circleCollider2D.isTrigger = true;
            _constantForce2D.enabled = false;
            _gravityController.enabled = false;
            _dashController.enabled = false;
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
                    if (currentGameObject.TryGetComponent(out ObjectController objectController))
                    {
                        var distance = Vector3.Distance(transform.position, objectController.transform.position);

                        if (distance < minDistance && objectController.CompareTag("Transitionable"))
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

            _isTransitioning = true;

            LeanTween.move(gameObject, transitableObject.transform, _transitionTime);
            LeanTween.scale(gameObject, Vector3.zero, _transitionTime).setOnComplete(OnTransitionCompleted);
        }

        private void OnTransitionCompleted()
        {
            Disable();

            _isTransitioning = false;

            _objectTransitionedTo.SetPlayer(this);
            
            OnTransitioned?.Invoke();
        }

        public void GetOutFromItem()
        {
            var targetPosition = _objectTransitionedTo.Transform.position + Vector3.down * 0.1f;

            _isTransitioning = true;

            LeanTween.move(gameObject, targetPosition, _transitionTime);

            LeanTween.scale(gameObject, Vector3.one, _transitionTime).setOnComplete(() =>
            {
                _isTransitioning = false;

                Enable();

                _objectTransitionedTo.PlayerGotOut();
                _objectTransitionedTo = null;

                _gravityController.SetGravity(false);
                
                OnPlayerGotOut?.Invoke();
            });
        }

        public void Die()
        {
            Disable();

            //TODO : Play Die Animation;

            GameManager.instance.LoseGame();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Deadly"))
            {
                Die();
            }

            if (collision.collider.CompareTag("Win"))
            {
                LeanTween.move(gameObject, collision.collider.transform, _transitionTime);
                LeanTween.scale(gameObject, Vector3.zero, _transitionTime).setOnComplete(GameManager.instance.WinGame);
            }
        }
    }

    [Serializable]
    public class PlayerStateEvent : UnityEvent<PlayerStateType>
    {
    }
}