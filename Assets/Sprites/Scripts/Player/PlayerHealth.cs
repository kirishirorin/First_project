using System;
using System.Collections;
using Sprites.Scripts.GameCore.Health;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : ObjectHealth
    {
        private WaitForSeconds _regenerationInterval  = new WaitForSeconds(5f);
        private float _regenerationValue = 1f;

        private void Start()
        {
            StartCoroutine(Regeneration());
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            if (CurrentHealth <= 0)
            {
                Debug.Log("Player is Dead");
            }
        }

        private IEnumerator Regeneration()
        {
            while (true)
            {
                TakeHeal(_regenerationValue);
                yield return _regenerationInterval;
            }
        }
    }
}