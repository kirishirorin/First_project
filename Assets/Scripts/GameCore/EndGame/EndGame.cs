using System;
using System.Collections;
using GameCore.Loot;
using GameCore.Pause;
using GameCore.UI;
using Player;
using Save;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace GameCore.EndGame
{
    public class EndGame : MonoBehaviour
    {
        [SerializeField] private Button _endGameButton;
        [SerializeField] private TMP_Text _coinsText;
        
        private WaitForSeconds _interval;
        private int _coins;
        
        private CoinsKeeper  _coinsKeeper;
        private RewardCoinsAnimation  _rewardCoinsAnimation;
        private PlayerData  _playerData;
        private SaveProgress _saveProgress;
        private GamePause  _gamePause;


        private void OnEnable()
        {
            _gamePause.SetPause(true);
            _endGameButton.gameObject.SetActive(false);
            _coins = _coinsKeeper.Coins;
            _coinsText.text = "0";
            _interval =  new WaitForSeconds(2.5f);
            StartCoroutine(CalculateCoins());
        }

        public void ExitGame()
        {
            _playerData.AddRewardCoins(_coins);
            _saveProgress.SaveData();
            SceneManager.LoadSceneAsync(0);
        }

        private IEnumerator CalculateCoins()
        {
            if (_coins > 10)
            {
                _rewardCoinsAnimation.ActivateAnimation(_coins, 0, _coinsText);
            }
            else
            {
                _coinsText.text = _coins.ToString();
                _endGameButton.gameObject.SetActive(true);
            }
            yield return _interval;
            _endGameButton.gameObject.SetActive(true);
        }


        [Inject]
        private void Construct(CoinsKeeper coinsKeeper,
                               RewardCoinsAnimation rewardCoinsAnimation,
                               PlayerData playerData,
                               SaveProgress saveProgress,
                               GamePause  gamePause)
        {
            _coinsKeeper = coinsKeeper;
            _rewardCoinsAnimation = rewardCoinsAnimation;
            _playerData = playerData;
            _saveProgress = saveProgress;
            _gamePause = gamePause;
        }
    }
}