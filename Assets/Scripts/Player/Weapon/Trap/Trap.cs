using System.Collections;
using Enemy;
using UnityEngine;
using Zenject;

namespace Player.Weapon.Trap
{
    public class Trap : BaseProjectileWeapon
    {
        [SerializeField] private CircleCollider2D _collider;
        [SerializeField] private float _disableDistance;
        private WaitForSeconds _checkInterval =  new WaitForSeconds(3f);
        private PlayerHealth  _playerHealth;
        private TrapWeapon _trapWeapon;


        protected override void OnEnable()
        {
            Damage = _trapWeapon.Damage;
            _collider.enabled = false;
            StartCoroutine(CheckDistance());
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                enemy.TakeDamage(Damage);
                if (enemy.gameObject.activeSelf)
                {
                    enemy.Born(Damage);
                }
                gameObject.SetActive(false);
            }
        }
        
        public void ActivateTrap() => _collider.enabled = true;

        private IEnumerator CheckDistance()
        {
            while (true)
            {
                if (Vector3.Distance(transform.position, _playerHealth.transform.position) > _disableDistance)
                {
                    gameObject.SetActive(false);
                }

                yield return _checkInterval;
            }
        }


        [Inject]
        private void Construct(PlayerHealth playerHealth, TrapWeapon trapWeapon)
        {
            _playerHealth = playerHealth;
            _trapWeapon = trapWeapon;
        }
    }
}