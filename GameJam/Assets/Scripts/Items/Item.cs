using DefaultNamespace;
using UnityEngine;

namespace Items
{
    public abstract class Item : MonoBehaviour , IItem
    {
        public ItemType Type => _type;

        [SerializeField]private ItemType _type;

        private IPlayer _player;
        
        public void Enable()
        {
            //TODO : Enable Buttons;
            
            enabled = true;
        }

        public void Disable()
        {
            //TODO : Disable Buttons;

            _player = null;
            enabled = false;
        }

        public abstract void Execute();

        public void SetPlayer(IPlayer player)
        {
            _player = player;

            Enable();

            //TODO : Maybe Enable Item Somewhere Else.
        }

        public void Destroy()
        {
            //TODO : Destroy Item Self.
            
            if(_player != null)
                _player.Die();
        }
        
        //TODO : OnTrigger Enter Check the ObjectType if its Deadly Destroy Object;
    }
}