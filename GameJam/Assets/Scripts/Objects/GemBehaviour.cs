using System;
using UnityEngine;

namespace Objects
{
    public class GemBehaviour : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                AddGem();
                Destroy(gameObject);
                return;
            }

            // if (collision.CompareTag("Transitionable"))
            // {
            //     if (collision.GetComponent<ObjectController>().IsEnable)
            //     {
            //         AddGem();
            //         Destroy(gameObject);
            //     }
            // }
        }
        
        private void AddGem()
        {
            PlayerPrefs.SetInt("Gems", PlayerPrefs.GetInt("Gems", 0) + 1);
        }
    }
}