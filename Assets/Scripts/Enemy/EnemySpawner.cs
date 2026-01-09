using System;
using System.Collections;
using Player;
using Sprites.Scripts.GameCore;
using GameCore.Pool;
using UnityEngine;
using Zenject;

namespace Sprites.Scripts.Enemy
{
    public class EnemySpawner : MonoBehaviour, IActivate
    {
        [SerializeField] private float _timeToSpawn;
        [SerializeField] private Transform _minPos, _maxPos;
        [SerializeField] private Transform _enemyContainer;
        [SerializeField] private ObjectPool _enemyPool;
        private PlayerMovement _playerMovement;
        private WaitForSeconds _interval;
        private GetRandomSpawnPoint _getRandomSpawn;
        private Coroutine _spawnCoroutine;

        private void Awake()
        {
            Activate();
        }

        private void Start() => _interval = new WaitForSeconds(_timeToSpawn);
        
        public void Activate() => _spawnCoroutine = StartCoroutine(Spawn());

        public void Deactivate()
        {
            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
            }
        }
        
        private IEnumerator Spawn()
        {
            while (true)
            {
                transform.position = _playerMovement.transform.position;
                GameObject enemy = _enemyPool.GetFromPool();
                enemy.transform.SetParent(_enemyContainer);
                enemy.transform.position = _getRandomSpawn.GetRandomPoint(_minPos, _maxPos);
                yield return _interval;
            }
        }

        [Inject] private void Construct(GetRandomSpawnPoint getRandomSpawn, PlayerMovement playerMovement)
        {
            _getRandomSpawn = getRandomSpawn;
            _playerMovement = playerMovement;
        }
    }
}