using Player;
using Sprites.Scripts.Player.Weapon.Suriken;
using UnityEngine;
using Zenject;

namespace Sprites.Scripts.DI
{
    public class PlayerInstaller: MonoInstaller

    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private SurikenWeapon _surikenWeapon;
        
        override public void InstallBindings()
        {
            Container.Bind<PlayerMovement>().FromInstance(_playerMovement).AsSingle().NonLazy();
            Container.Bind<PlayerHealth>().FromInstance(_playerHealth).AsSingle().NonLazy();
            Container.Bind<SurikenWeapon>().FromInstance(_surikenWeapon).AsSingle().NonLazy();
        }
    }
}