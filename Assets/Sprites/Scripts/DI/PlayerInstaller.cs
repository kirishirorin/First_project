using Player;
using UnityEngine;
using Zenject;

namespace Sprites.Scripts.DI
{
    public class PlayerInstaller: MonoInstaller

    {
        [SerializeField] private PlayerMovement _playerMovement;
        
        override public void InstallBindings()
        {
            Container.Bind<PlayerMovement>().FromInstance(_playerMovement).AsSingle().NonLazy();
        }
    }
}