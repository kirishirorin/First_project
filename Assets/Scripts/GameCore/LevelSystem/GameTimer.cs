using System;
using System.Collections;
using Sprites.Scripts.GameCore;
using TMPro;
using UnityEngine;
using Zenject;

namespace GameCore.LevelSystem
{
    public class GameTimer : MonoBehaviour, IActivate
    {
        [SerializeField] private TMP_Text _gameTimerText;
        private LevelSystem _levelSystem;
        private WaitForSeconds _tick =  new WaitForSeconds(1f);
        private Coroutine _timerCoroutine;
        private int _seconds, _minutes;
        
        public int Minutes => _minutes;

        private void Start()
        {
            Activate();
        }

        public void Activate()
        {
            _timerCoroutine = StartCoroutine(Timer());
        }

        public void Deactivate()
        {
            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);
            }
        }

        private IEnumerator Timer()
        {
            while (true)
            {
                _seconds++;
                if (_seconds >= 60)
                {
                    _minutes++;
                    _seconds = 0;
                    _levelSystem.OnLevelChanged?.Invoke();
                }
                TimeTextFormat();
                yield return _tick;
            }
        }

        private void TimeTextFormat()
        {
            _gameTimerText.text = $"{_minutes}:{_seconds}";
            if (_seconds < 10 && _minutes < 10)
            {
                _gameTimerText.text = $"0{_minutes}:0{_seconds}";
            }
            else if (_seconds < 10)
            {
                _gameTimerText.text = $"{_minutes}:0{_seconds}";
            }
            else if (_minutes < 10)
            {
                _gameTimerText.text = $"0{_minutes}:{_seconds}";
            }
        }
        
        [Inject] private void Construct(LevelSystem levelSystem)
        {
            _levelSystem = levelSystem;
        }
    }
}