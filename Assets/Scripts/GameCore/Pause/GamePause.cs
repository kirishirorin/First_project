using GameCore.LevelSystem;
using UnityEngine;
using Zenject;

namespace GameCore.Pause
{
    public class GamePause : MonoBehaviour
    {
        [SerializeField] private GameObject _playerWeapons;
        private LevelSystem.LevelSystem  _levelSystem;
        private GameTimer  _gameTimer;
        public bool IsStopped {get; private set;}
        
        
        public void SetPause(bool value)
        {
            if (value)
            {
                PauseOn();
            }
            else
            {
                PauseOff();
            }
        }

        private void PauseOn()
        {
            _levelSystem.Deactivate();
            _gameTimer.Deactivate();
            IsStopped =  true;
            _playerWeapons.SetActive(false);
        }

        private void PauseOff()
        {
            _levelSystem.Activate();
            _gameTimer.Activate();
            IsStopped = false;
            _playerWeapons.SetActive(true);
        }


        [Inject]
        private void Construct(LevelSystem.LevelSystem levelSystem, GameTimer gameTimer)
        {
            _levelSystem = levelSystem;
            _gameTimer = gameTimer;
        }
    }
}