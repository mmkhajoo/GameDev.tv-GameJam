using DefaultNamespace;
using UnityEngine;

namespace Items
{
    public abstract class Item : DragItem , IItem
    {
        [SerializeField]
        private ItemType _type;

        public ItemType Type => _type;

        public abstract bool IsActive { get; }
        
        public void Enable()
        {
            //TODO : Enable Buttons;
            
            enabled = true;
        }

        public void Disable()
        {
            //TODO : Disable Buttons;

            enabled = false;
        }

        public abstract void Execute();

        public void Active()
        {

        }

        public void DeActive()
        {

        }
    }
}