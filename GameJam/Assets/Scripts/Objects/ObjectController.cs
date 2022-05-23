using DefaultNamespace;
using UnityEngine;

namespace Objects
{
    public class ObjectController : MonoBehaviour , IObject
    {
        private IPlayer _player;

        public bool IsEnable { get; private set; }
        
        public void SetPlayer(IPlayer player)
        {
            _player = player;

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
        }

        protected virtual void Disable()
        {
            IsEnable = false;
        }

        #endregion
        
        //TODO : OnTrigger Enter Check the ObjectType if its Deadly Destroy Object;
    }
}