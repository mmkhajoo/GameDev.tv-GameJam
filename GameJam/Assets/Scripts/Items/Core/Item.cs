using DefaultNamespace;
using UnityEngine;

namespace Items
{
    public abstract class Item : DragItem , IItem
    {
        [SerializeField]
        private ItemType _type;
        private bool _isActive;
        private bool _isEnable;

        public ItemType Type => _type;

        public bool IsActive => _isActive;

        public bool IsEnable => _isEnable;

        public abstract void Initialize();
        public abstract void Execute();

        public virtual void Enable()
        {
            _isEnable = true;
            gameObject.SetActive(true);
        }

        public virtual void Disable()
        {
            _isEnable = false;
            gameObject.SetActive(false);
        }


        public virtual void Active()
        {
            _isActive = true;
        }

        public virtual void DeActive()
        {
            _isActive = false;
        }


        protected override void Update()
        {
            base.Update();

            if (IsActive)
                Execute();
        }
    }
}