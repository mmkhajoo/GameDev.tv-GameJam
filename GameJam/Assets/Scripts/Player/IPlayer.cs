using System;
using Objects;

namespace DefaultNamespace
{
    public interface IPlayer
    {
        event Action<PlayerStateType> OnPlayerStateChanged;
        event Action OnPlayerJumped;
        
        void Enable();

        void Disable();

        void Transition(ObjectController gameObject);

        void GetOutFromItem();

        void Die();
    }
}