using System;
using System.Collections;
using System.Collections.Generic;
using Sprites.Scripts.Enemy;
using Sprites.Scripts.GameCore;
using UnityEngine;

namespace Sprites.Scripts.Player.Weapon
{
    public class AuraWeapon : BaseWeapon, IActivate
    {
        [SerializeField] private Transform _targetContainer;
        [SerializeField] private CircleCollider2D _collider;
        private List<EnemyHealth> _enemyInZone = new List<EnemyHealth>();
        private WaitForSeconds _timeBetweenAttacks;
        private Coroutine _auraCoroutine;
        private float _range;


        protected override void Start()
        {
            base.Start();
            Activate();
        }


        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                _enemyInZone.Add(enemy);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                _enemyInZone.Remove(enemy);
            }
        }
        
        
        public void Activate()
        {
            SetStats(CurrentLevel);
            _auraCoroutine = StartCoroutine(CheckZone());
        }

        public void Deactivate()
        {
            if (_auraCoroutine != null)
            {
                StopCoroutine(_auraCoroutine);
            }
        }


        protected override void SetStats(int level)
        {
            base.SetStats(level);
            _damage = 3f;
            _timeBetweenAttacks = new WaitForSeconds(1f);//new WaitForSeconds(WeaponStats[CurrentLevel - 1].TimeBetweenAttacks);
            _range = 5f;//WeaponStats[CurrentLevel - 1].Range;
            _targetContainer.transform.localScale = Vector3.one *  _range;
            _collider.radius = _range / 3.1f;
        }


        private IEnumerator CheckZone()
        {
            while (true)
            {
                for (int i = 0; i < _enemyInZone.Count; i++)
                {
                    _enemyInZone[i].TakeDamage(_damage);
                }
                
                yield return _timeBetweenAttacks;
            }
        }
        
    }
}