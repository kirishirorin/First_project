using Player;
using UnityEngine;
using Zenject;

namespace Save
{
    public class SaveProgress
    {
        private PlayerData  _playerData;
        public void SaveData()
        {
            PlayerPrefs.SetInt("Coins", _playerData.Coins);
            PlayerPrefs.SetInt("Health", _playerData.MaxHealthUpgradeIndex);
            PlayerPrefs.SetInt("Speed", _playerData.SpeedUpgradeIndex);
            PlayerPrefs.SetInt("Regeneration", _playerData.RegenerationUpgradeIndex);
            PlayerPrefs.SetInt("ExpRange", _playerData.ExpRangeUpgradeIndex);
        }

        public void LoadData()
        {
            _playerData.AddRewardCoins(PlayerPrefs.GetInt("Coins"));
            _playerData.SetUpgradeIndex(PlayerPrefs.GetInt("Health"), 1);
            if (PlayerPrefs.GetInt("Health") == 0)
            {
                _playerData.SetUpgradeIndex(1, 1);
            }
            _playerData.SetUpgradeIndex(PlayerPrefs.GetInt("Speed"), 2);
            if (PlayerPrefs.GetInt("Speed") == 0)
            {
                _playerData.SetUpgradeIndex(1, 1);
            }
            _playerData.SetUpgradeIndex(PlayerPrefs.GetInt("Regeneration"), 3);
            if (PlayerPrefs.GetInt("Regeneration") == 0)
            {
                _playerData.SetUpgradeIndex(1, 1);
            }
            _playerData.SetUpgradeIndex(PlayerPrefs.GetInt("ExpRange"), 4);
            if (PlayerPrefs.GetInt("ExpRange") == 0)
            {
                _playerData.SetUpgradeIndex(1, 1);
            }
        }
        
        
        [Inject]
        public void Construct(PlayerData playerData)
        {
            _playerData = playerData;
        }
    }
}