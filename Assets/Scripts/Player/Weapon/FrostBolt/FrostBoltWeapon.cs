using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using GameCore.Pool;
using Sprites.Scripts.GameCore;
using UnityEngine;
using UnityEngine.UI;

namespace Player.Weapon.FrostBolt
{
    public class FrostBoltWeapon : BaseWeapon, IActivate
    {
        [SerializeField] private ObjectPool _objectPool;
        [SerializeField] private Transform _container;
        [SerializeField] private List<Transform> _shootPoints = new List<Transform>();
        private WaitForSeconds _timeBetweenAttack;
        private Coroutine _frostBoltCoroutine;
        private float _duration, _speed;
        private Vector3 _direction;
        [SerializeField] private Text _frozenBoltLevelText;

        public float Speed => _speed;
        public Vector3 Direction => _direction;
        public float Duration => _duration;


        private void OnEnable()
        {
            Activate();
        }


        public void Activate()
        {
            SetStats(1);
            _frostBoltCoroutine = StartCoroutine(StartThrowFrozenBolt());
        }

        public void Deactivate()
        {
            if (_frostBoltCoroutine != null)
            {
                StopCoroutine(_frostBoltCoroutine);
            }
        }

        protected override void SetStats(int level)
        {
            base.SetStats(level);
            _timeBetweenAttack = new WaitForSeconds(WeaponStats[level - 1].TimeBetweenAttacks);
            _speed = WeaponStats[level - 1].Speed;
            _duration = WeaponStats[level - 1].Duration;
            _frozenBoltLevelText.text = level.ToString();
        }

        private IEnumerator StartThrowFrozenBolt()
        {
            while (true)
            {
                for (int i = 0; i < _shootPoints.Count; i++)
                {
                    _direction = (_shootPoints[i].position - transform.position).normalized;
                    float angle = Mathf.Atan2(_direction.y,  _direction.x) * Mathf.Rad2Deg;
                    GameObject frozenBolt = _objectPool.GetFromPool();
                    frozenBolt.transform.SetParent(_container);
                    frozenBolt.transform.position = _shootPoints[i].position;
                    frozenBolt.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
                yield return _timeBetweenAttack;
            }
        }
    }
}