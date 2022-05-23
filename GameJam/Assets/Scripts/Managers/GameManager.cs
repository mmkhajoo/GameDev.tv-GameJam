using System;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public Transform playerDefaultParent;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
    }
}