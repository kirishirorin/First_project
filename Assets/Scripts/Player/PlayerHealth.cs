using System;
using System.Collections;
using GameCore.Health;
using GameCore.Pause;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerHealth : ObjectHealth
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _endGameWindow;
        
        public Action OnHealthChanged;
        private WaitForSeconds _regenerationInterval  = new WaitForSeconds(5f);
        private float _regenerationValue = 1f;
        private WaitForSeconds _interval  = new WaitForSeconds(1f);
        
        private GamePause  _gamePause;

        private void Start() => StartCoroutine(Regeneration());

        public void Heal(float value)
        {
            TakeHeal(value);
            OnHealthChanged?.Invoke();
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            OnHealthChanged?.Invoke();
            if (CurrentHealth <= 0)
            {
                StartCoroutine(PlayerDied());
            }
        }

        public void UpgradeHealth()
        {
            _maxHealth += 10;
            _currentHealth += 10;
        }

        public void UpgradeRegeneration()
        {
            _regenerationValue++;
        }

        private IEnumerator Regeneration()
        {
            while (true)
            {
                TakeHeal(_regenerationValue);
                OnHealthChanged?.Invoke();
                yield return _regenerationInterval;
            }
        }

        private IEnumerator PlayerDied()
        {
            _gamePause.SetPause(true);
            _animator.SetTrigger("Die");
            yield return _interval;
            _endGameWindow.SetActive(true);
        }


        [Inject]
        private void Construct(GamePause gamePause)
        {
            _gamePause = gamePause;
        }
    }
}