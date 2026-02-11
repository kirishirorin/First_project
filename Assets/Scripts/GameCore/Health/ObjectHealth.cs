using System;
using UnityEngine;

namespace GameCore.Health
{
    public abstract class ObjectHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] protected float _maxHealth;
        [SerializeField] protected float _currentHealth;

        public float MaxHealth => _maxHealth;
        public float CurrentHealth => _currentHealth;

        private void OnEnable()
        {
            _currentHealth = _maxHealth;
        }

        public virtual void TakeDamage(float damage)
        {
            if (damage <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(damage));
            }
            else
            {
                _currentHealth -= damage;
            }
        }

        public void TakeHeal(float heal)
        {
            if (heal <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(heal));
            }
            else if (_currentHealth + heal > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
            else
            {
                _currentHealth += heal;
            }
        }
    }
}