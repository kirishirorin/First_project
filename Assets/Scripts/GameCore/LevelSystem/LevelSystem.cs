using System;
using System.Collections.Generic;
using Sprites.Scripts.GameCore;
using UnityEngine;
using Zenject;

namespace GameCore.LevelSystem
{
    public class LevelSystem : MonoBehaviour, IActivate
    {
        public Action OnLevelChanged; // event Action
        
        [SerializeField] private List<Level> _levels = new List<Level>();
        private GameTimer _gameTimer;
        private DiContainer _diContainer;

        private void Awake()
        {
            for (int i = 0; i < _levels.Count; i++)
            {
                _diContainer.Inject(_levels);
            }
        }

        private void Start()
        {
            Activate();
        }

        private void OnEnable()
        {
            OnLevelChanged += LevelUp;
        }

        private void OnDisable()
        {
            OnLevelChanged -= LevelUp;
        }

        public void Activate()
        {
            _levels[_gameTimer.Minutes].Activate();
        }

        public void Deactivate()
        {
            _levels[_gameTimer.Minutes].Deactivate();
        }

        private void LevelUp()
        {
            _levels[_gameTimer.Minutes].Deactivate();
            Activate();
        }
        
        [Inject] private void Construct(GameTimer gameTimer,  DiContainer diContainer)
        {
            _gameTimer = gameTimer;
            _diContainer = diContainer;
        }
    }
}