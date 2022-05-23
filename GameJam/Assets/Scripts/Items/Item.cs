using DefaultNamespace;
using UnityEngine;

namespace Items
{
    public abstract class Item : MonoBehaviour , IItem
    {
        public ItemType Type => _type;

        [SerializeField]private ItemType _type;
        
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
    }
}