using GameCore.UI;
using Sprites.Scripts.GameCore;
using UnityEngine;
using Zenject;

namespace Sprites.Scripts.DI
{
    public class GameInstaller: MonoInstaller
    {
        [SerializeField] private DamageTextSpawner _damageTextSpawner;
        public override void InstallBindings()
        {
            Container.Bind<GetRandomSpawnPoint>().FromNew().AsSingle().NonLazy();
            Container.Bind<DamageTextSpawner>().FromInstance(_damageTextSpawner).AsSingle().NonLazy();
        }
    }
}