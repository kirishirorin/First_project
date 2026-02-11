


using System;
using Player;
using Player.Weapon;
using Player.Weapon.Bow;
using Player.Weapon.FrostBolt;
using Player.Weapon.Suriken;
using Player.Weapon.Trap;
using UnityEngine;
using Zenject;

namespace GameCore.UpgradeSystem
{
    public class PlayerUpgrade : UnityEngine.MonoBehaviour
    {
        private PlayerHealth _playerHealth;
        private PlayerMovement _playerMovement;
        private FireBallWeapon _fireBallWeapon;
        private AuraWeapon _auraWeapon;
        private SurikenWeapon _surikenWeapon;
        private FrostBoltWeapon _frostBoltWeapon;
        private TrapWeapon _trapWeapon;
        private BowWeapon _bowWeapon;
        
        public FireBallWeapon FireBallWeapon => _fireBallWeapon;
        public AuraWeapon AuraWeapon => _auraWeapon;
        public SurikenWeapon SurikenWeapon => _surikenWeapon;
        public FrostBoltWeapon FrostBoltWeapon => _frostBoltWeapon;
        public TrapWeapon TrapWeapon => _trapWeapon;
        public BowWeapon BowWeapon => _bowWeapon;
        
        public float RangeExp {get; private set;}


        private void Start() => RangeExp = 1.5f;

        public void UpgradeHealth() => _playerHealth.UpgradeHealth();
        public void UpgradeRegeneration() => _playerHealth.UpgradeRegeneration();
        public void UpgradeSpeed() => _playerMovement.UpgradeSpeed();
        public void UpgradeExpRange() => RangeExp += 1f;

        public void UpgradeWeapon(BaseWeapon weapon)
        {
            if (weapon.gameObject.activeSelf)
            {
                weapon.LevelUp();
            }
            else
            {
                ActivateWeapon(weapon);
            }
        }

        private void ActivateWeapon(BaseWeapon weapon)
        {
            weapon.gameObject.SetActive(true);
        }

        [Inject]
        private void Construct(PlayerHealth playerHealth, PlayerMovement playerMovement, FireBallWeapon fireBallWeapon,
            AuraWeapon auraWeapon, SurikenWeapon surikenWeapon, FrostBoltWeapon frostBoltWeapon, TrapWeapon trapWeapon,
            BowWeapon bowWeapon)
        {
            _playerHealth = playerHealth;
            _playerMovement =  playerMovement;
            _fireBallWeapon = fireBallWeapon;
            _auraWeapon = auraWeapon;
            _surikenWeapon = surikenWeapon;
            _frostBoltWeapon = frostBoltWeapon;
            _trapWeapon = trapWeapon;
            _bowWeapon = bowWeapon;
        }
    }
}