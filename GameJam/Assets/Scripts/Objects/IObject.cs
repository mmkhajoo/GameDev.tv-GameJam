using DefaultNamespace;
using UnityEngine;

namespace Objects
{
    public interface IObject
    {
        Transform Transform { get; }
        Collider2D Collider2D { get; }

        Rigidbody2D Rigidbody2D { get; }

        bool IsEnable { get; }

        ObjectType ObjectType { get; }
        ObjectFeatureType ObjectFeatureType { get; }

        void SetPlayer(IPlayer player);

        void PlayerGotOut();
        
        void Destroy();
    }
}