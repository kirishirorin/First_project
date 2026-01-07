using System;
using System.Collections;
using Sprites.Scripts.Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sprites.Scripts.Player.Weapon
{
    public abstract class BaseProjectileWeapon : MonoBehaviour
    {
        protected WaitForSeconds Timer;
        protected float Damage;


        protected virtual void OnEnable() => StartCoroutine(TimerToHide());


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                float damage = Random.Range(Damage / 1.5f, Damage * 1.5f);
                if (damage < 1)
                {
                    damage = 1;
                }
                enemy.TakeDamage(damage);
            }
        }


        private IEnumerator TimerToHide()
        {
            yield return Timer;
            gameObject.SetActive(false);
        }
    }
}