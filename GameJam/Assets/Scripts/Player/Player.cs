using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Player : MonoBehaviour , IPlayer
    {
        private PlayerMovement _playerMovement;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }

        public void Enable()
        {
            _playerMovement.enabled = true;
        }

        public void Disable()
        {
            _playerMovement.enabled = false;
        }

        public void Transition()
        {
            //TODO : Set Item Function For Transition.
            //TODO : Set Player Game Object Child of The Item;
        }

        public void GetOutFromItem()
        {
            
        }

        public void Die()
        {
            //TODO : Create Event and and fire it when player die.
        }
        
        //TODO : Search for Item Around Player for Transition in Update Method.
    }
}