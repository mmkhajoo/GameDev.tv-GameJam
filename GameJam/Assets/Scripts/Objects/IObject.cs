using DefaultNamespace;

namespace Objects
{
    public interface IObject
    {
        bool IsEnable { get; }
        
        void SetPlayer(IPlayer player);

        void PlayerGotOut();
        
        void Destroy();
    }
}