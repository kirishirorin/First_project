using System;
using System.Collections;
using Sprites.Scripts.Player.Weapon;
using UnityEngine;
using Zenject;

namespace Player.Weapon.Suriken
{
    public class Suriken : BaseProjectileWeapon
    {
        [SerializeField] private Transform _surikenSprite;
        private SurikenWeapon _surikenWeapon;
        private PlayerMovement _playerMovement;
        private int part = 1;


        protected override void OnEnable()
        {
            ChoosePart();
            Timer = new WaitForSeconds(_surikenWeapon.Duration);
            Damage = _surikenWeapon.Damage;
        }


        private void Update()
        {
            _surikenSprite.transform.Rotate(0,0,500f * Time.deltaTime);
            if (part == 1)
            {
                transform.position += transform.right * (_surikenWeapon.Speed * Time.deltaTime);
            }
            else if (part == 2)
            {
                Vector3 _direction = (_playerMovement.transform.position - transform.position).normalized;
                transform.position += _direction * (_surikenWeapon.Speed * Time.deltaTime);
            }
        }

        private void ChoosePart()
        {
            if (_surikenWeapon.CurrentLevel < 5)
            {
                StartCoroutine(TimerToHide());
            }
            else
            {
                StartCoroutine(TimerToPart1());
            }
        }

        private IEnumerator TimerToPart1()
        {
            yield return Timer;
            part = 2;
            StartCoroutine(TimerToPart2());
        }

        private IEnumerator TimerToPart2()
        {
            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);
            part = 1;
        }

        private IEnumerator TimerToHide()
        {
            yield return Timer;
            gameObject.SetActive(false);
        }


        [Inject] private void Construct(SurikenWeapon surikenWeapon, PlayerMovement playerMovement)
        {
            _surikenWeapon = surikenWeapon;
            _playerMovement = playerMovement;
        }
    }
}