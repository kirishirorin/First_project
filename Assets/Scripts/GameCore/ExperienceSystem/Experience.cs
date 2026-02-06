using System;
using GameCore.UpgradeSystem;
using Player;
using UnityEngine;
using Zenject;

namespace GameCore.ExperienceSystem
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] private int _value;
        private ExperienceSystem _experienceSystem;
        private PlayerHealth  _playerHealth;
        private PlayerUpgrade  _playerUpgrade;
        private float _distanceToPickUp = 1.5f;


        private void OnEnable()
        {
            _distanceToPickUp = _playerUpgrade.RangeExp;
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, _playerHealth.transform.position) <= _distanceToPickUp)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    _playerHealth.transform.position, 2f * Time.deltaTime);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerHealth playerHealth))
            {
                _experienceSystem.OnExperiencePickup?.Invoke(_value);
                gameObject.SetActive(false);
            }
        }

        [Inject]
        private void Construct(ExperienceSystem experienceSystem, PlayerHealth playerHealth, PlayerUpgrade playerUpgrade)
        {
            _experienceSystem  = experienceSystem;
            _playerHealth = playerHealth;
            _playerUpgrade = playerUpgrade;
        }
    }
}