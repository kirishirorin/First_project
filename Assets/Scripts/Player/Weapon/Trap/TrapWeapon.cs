using System;
using System.Collections;
using GameCore.Pool;
using Player.Weapon;
using Sprites.Scripts.GameCore;
using UnityEngine;

namespace Player.Weapon.Trap
{
    public class TrapWeapon : BaseWeapon, IActivate
    {
        [SerializeField] private ObjectPool _objectPool;
        [SerializeField] private Transform _container;
        private WaitForSeconds _timeBetweenAttack;
        private Coroutine _trapCoroutine;


        private void OnEnable() => Activate();


        public void Activate()
        {
            SetStats(1);
            _trapCoroutine = StartCoroutine(SpawnTrap());
        }

        public void Deactivate()
        {
            if (_trapCoroutine != null)
            {
                StopCoroutine(_trapCoroutine);
            }
        }

        protected override void SetStats(int level)
        {
            base.SetStats(CurrentLevel);
            _timeBetweenAttack = new WaitForSeconds(WeaponStats[CurrentLevel - 1].TimeBetweenAttacks);
        }

        private IEnumerator SpawnTrap()
        {
            while (true)
            {
                GameObject trap = _objectPool.GetFromPool();
                trap.transform.SetParent(_container);
                trap.transform.position = transform.position;
                yield return _timeBetweenAttack;
            }
        }
    }
}