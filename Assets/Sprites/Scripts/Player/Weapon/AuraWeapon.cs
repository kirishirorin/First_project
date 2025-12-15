using System;
using System.Collections;
using System.Collections.Generic;
using Sprites.Scripts.Enemy;
using Sprites.Scripts.GameCore;
using UnityEngine;
using UnityEngine.UI;

namespace Sprites.Scripts.Player.Weapon
{
    public class AuraWeapon : BaseWeapon, IActivate
    {
        [SerializeField] private Transform _targetContainer;
        [SerializeField] private CircleCollider2D _collider;
        [SerializeField] private Text _auraLevelText;
        private List<EnemyHealth> _enemyInZoneHealth = new List<EnemyHealth>();
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
            if (other.gameObject.TryGetComponent(out EnemyHealth enemyHealth))
            {
                _enemyInZoneHealth.Add(enemyHealth);
            }
            if (CurrentLevel >= 5 && other.gameObject.TryGetComponent(out EnemyMovement enemyMovement))
            {
                enemyMovement.MoveSpeed = enemyMovement.MoveSpeed / 2;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemyHealth))
            {
                _enemyInZoneHealth.Remove(enemyHealth);
            }
            if (CurrentLevel >= 5 && other.gameObject.TryGetComponent(out EnemyMovement enemyMovement))
            {
                enemyMovement.MoveSpeed = enemyMovement.MoveSpeed * 2;
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
            _timeBetweenAttacks = new WaitForSeconds(WeaponStats[CurrentLevel - 1].TimeBetweenAttacks);
            _range = WeaponStats[CurrentLevel - 1].Range;
            _targetContainer.transform.localScale = Vector3.one *  _range;
            _collider.radius = _range / 3.1f;
        }


        private IEnumerator CheckZone()
        {
            while (true)
            {
                for (int i = 0; i < _enemyInZoneHealth.Count; i++)
                {
                    _enemyInZoneHealth[i].TakeDamage(_damage);
                }
                LevelUp();
                _auraLevelText.text = CurrentLevel.ToString();
                
                yield return _timeBetweenAttacks;
            }
        }
        
    }
}