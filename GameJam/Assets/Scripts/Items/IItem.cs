using DefaultNamespace;

namespace Items
{
    public interface IItem
    {
        ItemType Type { get; }

        void Enable();

        void Disable();

        void Execute();
    }
}