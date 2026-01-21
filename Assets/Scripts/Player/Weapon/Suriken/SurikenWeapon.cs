using System;
using System.Collections;
using Player;
using GameCore.Pool;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;
using Zenject;
using Random = UnityEngine.Random;

namespace Player.Weapon.Suriken
{
    public class SurikenWeapon : BaseWeapon
    {
        [SerializeField] private ObjectPool _objectPool;
        [SerializeField] private Transform _container;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Text _surikenLevelText;
        private WaitForSeconds _timeBetweenAttacks;
        private Coroutine _surikenCoroutine;
        private float _duration, _speed, _range;
        private Vector3 _direction;

        public float Duration => _duration;
        public float Speed => _speed;
        public Vector3 Direction => _direction;


        private void OnEnable()
        {
            Activate();
        }


        public void Activate()
        {
            SetStats(CurrentLevel);
            _surikenCoroutine = StartCoroutine(SpawnSuriken());
        }

        public void Deactivate()
        {
            if (_surikenCoroutine != null)
            {
                StopCoroutine(_surikenCoroutine);
            }
        }


        protected override void SetStats(int level)
        {
            base.SetStats(level);
            _timeBetweenAttacks  = new WaitForSeconds(WeaponStats[level - 1].TimeBetweenAttacks);
            _speed = WeaponStats[level - 1].Speed;
            _range = WeaponStats[level - 1].Range;
            _duration = WeaponStats[level - 1].Duration;
            _surikenLevelText.text = level.ToString();
        }


        private IEnumerator SpawnSuriken()
        {
            while (true)
            {
                Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, _range, _layerMask);
                if (enemiesInRange.Length > 0)
                {
                    Vector3 targetPosition = enemiesInRange[Random.Range(0, enemiesInRange.Length)].transform.position;
                    _direction = (targetPosition - transform.position).normalized;
                    float angle = MathF.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
                    GameObject suriken = _objectPool.GetFromPool();
                    suriken.transform.SetParent(_container);
                    suriken.transform.position = transform.position;
                    suriken.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    yield return _timeBetweenAttacks;
                }
                else
                {
                    yield return _timeBetweenAttacks;
                }
            }
        }
    }
}