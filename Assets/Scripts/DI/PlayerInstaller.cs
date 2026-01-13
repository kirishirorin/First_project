using Player;
using Player.Weapon.Bow;
using Player.Weapon.FrostBolt;
using Player.Weapon.Suriken;
using Player.Weapon.Trap;
using UnityEngine;
using Zenject;

namespace DI
{
    public class PlayerInstaller: MonoInstaller

    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private SurikenWeapon _surikenWeapon;
        [SerializeField] private FrostBoltWeapon _frostBoltWeapon;
        [SerializeField] private TrapWeapon _trapWeapon;
        [SerializeField] private BowWeapon _bowWeapon;
        
        override public void InstallBindings()
        {
            Container.Bind<PlayerMovement>().FromInstance(_playerMovement).AsSingle().NonLazy();
            Container.Bind<PlayerHealth>().FromInstance(_playerHealth).AsSingle().NonLazy();
            Container.Bind<SurikenWeapon>().FromInstance(_surikenWeapon).AsSingle().NonLazy();
            Container.Bind<FrostBoltWeapon>().FromInstance(_frostBoltWeapon).AsSingle().NonLazy();
            Container.Bind<TrapWeapon>().FromInstance(_trapWeapon).AsSingle().NonLazy();
            Container.Bind<BowWeapon>().FromInstance(_bowWeapon).AsSingle().NonLazy();
        }
    }
}