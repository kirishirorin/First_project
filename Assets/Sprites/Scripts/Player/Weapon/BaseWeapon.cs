using System;
using System.Collections.Generic;
using Sprites.Scripts.Enemy;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Sprites.Scripts.Player.Weapon
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        [SerializeField] private List<WeaponStats> _weaponStats = new List<WeaponStats>();
        protected float _damage;
        private DiContainer _diContainer;
        private int _currentLevel = 1;
        private int _maxLevel = 8;

        public List<WeaponStats> WeaponStats => _weaponStats;
        public int CurrentLevel => _currentLevel;
        public int MaxLevel => _maxLevel;


        private void Awake() => _diContainer.Inject(this);

        protected virtual void Start() => SetStats(_currentLevel);


        public virtual void LevelUp()
        {
            if (_currentLevel < _maxLevel)
            {
                _currentLevel++;
            }
            SetStats(_currentLevel);
        }


        protected virtual void SetStats(int level)
        {
            _damage = 5f; //WeaponStats[level-1].Damage;
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                float damage = Random.Range(_weaponStats[_currentLevel].Damage / 2f, _weaponStats[_currentLevel].Damage * 1.5f);
                enemy.TakeDamage(damage);
            }
        }


        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
    }
}