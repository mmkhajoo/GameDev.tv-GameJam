namespace DefaultNamespace
{
    public interface IPlayer
    {
        void Enable();

        void Disable();

        void Transition(/*Item Input*/);

        void GetOutFromItem();

        void Die();
    }
}