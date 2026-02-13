using System;
using System.Collections;
using GameCore.Loot;
using GameCore.Pause;
using Sprites.Scripts.GameCore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace GameCore.UI
{
    public class TreasureWindow : MonoBehaviour, IActivate
    {
        [SerializeField] private GameObject _treasureWindow;
        [SerializeField] private TMP_Text _coinsText;
        [SerializeField] private Button _button;
        private GamePause _gamePause;
        private CoinsKeeper _coinsKeeper;
        private CoinsUIUpdater _coinsUIUpdater;
        private RewardCoinsAnimation  _rewardCoinsAnimation;
        private int _randomCoinsToAdd;
        private WaitForSeconds _interval;


        private void Start()
        {
            _interval = new WaitForSeconds(2.5f);
        }


        public void Activate()
        {
            _treasureWindow.SetActive(true);
            _gamePause.SetPause(true);
            _button.gameObject.SetActive(false);
            _randomCoinsToAdd = (int)Random.Range(10f, 100f);
            StartCoroutine(StartCalculate());
        }

        public void Deactivate()
        {
            _treasureWindow.SetActive(false);
            _gamePause.SetPause(false);
        }

        public void GetReward()
        {
            _coinsKeeper.AddCoins(_randomCoinsToAdd);
            _coinsUIUpdater.OnCountChanged?.Invoke();
        }

        private IEnumerator StartCalculate()
        {
            _rewardCoinsAnimation.ActivateAnimation(_randomCoinsToAdd, _coinsKeeper.Coins, _coinsText);
            yield return _interval;
            GetReward();
            _button.gameObject.SetActive(true);
        }
        
        
        [Inject]
        public void Construct(GamePause gamePause,
                              CoinsKeeper coinsKeeper,
                              CoinsUIUpdater coinsUIUpdater,
                              RewardCoinsAnimation  rewardCoinsAnimation)
        {
            _gamePause = gamePause;
            _coinsKeeper = coinsKeeper;
            _coinsUIUpdater = coinsUIUpdater;
            _rewardCoinsAnimation = rewardCoinsAnimation;
        }
    }
}