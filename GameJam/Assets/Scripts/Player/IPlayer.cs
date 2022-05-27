using System;
using Objects;

namespace DefaultNamespace
{
    public interface IPlayer
    {
        void Enable();

        void Disable();

        void Transition(ObjectController gameObject);

        void GetOutFromItem();

        void Die();
    }
}