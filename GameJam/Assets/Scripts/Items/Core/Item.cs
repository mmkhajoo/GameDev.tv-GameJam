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

        public abstract void Execute();

        public void Enable()
        {
            //TODO : Enable Buttons;
            _isEnable = true;
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            //TODO : Disable Buttons;
            _isEnable = false;
            gameObject.SetActive(false);
        }


        public virtual void Active()
        {
            _isActive = true;
            enabled = true;
        }

        public virtual void DeActive()
        {
            _isActive = false;
            enabled = false;
        }
    }
}