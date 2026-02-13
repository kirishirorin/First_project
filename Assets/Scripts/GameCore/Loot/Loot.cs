using System;
using Player;
using UnityEngine;

namespace GameCore.Loot
{
    public abstract class Loot : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerHealth player))
            {
               Pickup(); 
            }
        }

        protected virtual void Pickup()
        {
            gameObject.SetActive(false);
        }
    }
}