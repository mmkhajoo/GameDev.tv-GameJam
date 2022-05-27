using DefaultNamespace;
using UnityEngine;

namespace Objects
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class ObjectController : MonoBehaviour , IObject
    {
        #region Properties

        private IPlayer _player;
        public Transform Transform => transform;
        public Collider2D Collider2D => _collider2D;
        public Rigidbody2D Rigidbody2D => _rigidbody2D;
        public bool IsEnable { get; private set; }
        public ObjectType ObjectType => _objectType;
        public ObjectFeatureType ObjectFeatureType => _objectFeatureType;
        
        #endregion
        
        
        [SerializeField]
        private ObjectType _objectType;

        [SerializeField] private ObjectFeatureType _objectFeatureType;

        private Collider2D _collider2D;
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<Collider2D>();
            Disable();
        }

        public void SetPlayer(IPlayer player)
        {
            _player = player;

            Enable();

            //TODO : Maybe Enable Item Somewhere Else.
        }

        public void PlayerGotOut()
        {
            _player = null;
            
            Disable();
        }

        public void Destroy()
        {
            //TODO : Destroy Object Self.
            
            if(_player != null)
                _player.Die();
        }

        #region Private Methods

        protected virtual void Enable()
        {
            IsEnable = true;
            _rigidbody2D.isKinematic = false;
        }

        protected virtual void Disable()
        {
            IsEnable = false;
            
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.isKinematic = true;
        }

        #endregion
        
        //TODO : OnTrigger Enter Check the ObjectType if its Deadly Destroy Object;
    }
}