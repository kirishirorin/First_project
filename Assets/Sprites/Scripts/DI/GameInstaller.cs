using Sprites.Scripts.GameCore;
using Zenject;

namespace Sprites.Scripts.DI
{
    public class GameInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GetRandomSpawnPoint>().FromNew().AsSingle().NonLazy();
        }
    }
}