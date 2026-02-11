using GameCore.ExperienceSystem;
using GameCore.LevelSystem;
using GameCore.Pause;
using GameCore.UI;
using GameCore.UpgradeSystem;
using Sprites.Scripts.GameCore;
using UnityEngine;
using Zenject;

namespace DI
{
    public class GameInstaller: MonoInstaller
    {
        [SerializeField] private DamageTextSpawner _damageTextSpawner;
        [SerializeField] private LevelSystem _levelSystem;
        [SerializeField] private PlayerUpgrade _playerUpgrade;
        [SerializeField] private GameTimer _gameTimer;
        [SerializeField] private ExperienceSpawner _experienceSpawner;
        [SerializeField] private ExperienceSystem _experienceSystem;
        [SerializeField] private UpgradeWindow _upgradeWindow;
        [SerializeField] private GamePause _gamePause;
        public override void InstallBindings()
        {
            LevelSystem();
            Container.Bind<GetRandomSpawnPoint>().FromNew().AsSingle().NonLazy();
            Container.Bind<DamageTextSpawner>().FromInstance(_damageTextSpawner).AsSingle().NonLazy();
            Container.Bind<GameTimer>().FromInstance(_gameTimer).AsSingle().NonLazy();
            Container.Bind<ExperienceSpawner>().FromInstance(_experienceSpawner).AsSingle().NonLazy();
            Container.Bind<PlayerUpgrade>().FromInstance(_playerUpgrade).AsSingle().NonLazy();
            Container.Bind<ExperienceSystem>().FromInstance(_experienceSystem).AsSingle().NonLazy();
            Container.Bind<UpgradeWindow>().FromInstance(_upgradeWindow).AsSingle().NonLazy();
            Container.Bind<GamePause>().FromInstance(_gamePause).AsSingle().NonLazy();
        }

        private void LevelSystem()
        {
            Container.Bind<LevelSystem>().FromInstance(_levelSystem).AsSingle().Lazy();
        }
    }
}