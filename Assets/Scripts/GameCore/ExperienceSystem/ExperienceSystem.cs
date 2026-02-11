using System;
using GameCore.UpgradeSystem;
using UnityEngine;

namespace GameCore.ExperienceSystem
{
    public class ExperienceSystem : MonoBehaviour
    {
        public Action<float> OnExperiencePickup;
        [SerializeField] private GameObject _upgradeWindow;
        private float _currentExperience, _experienceToUp = 5, _currentLevel = 1;

        public float CurrentExperience => _currentExperience;
        public float ExperienceToUp => _experienceToUp;
        public float CurrentLevel => _currentLevel;


        private void OnEnable()
        {
            OnExperiencePickup += ExperienceAddValue;
        }

        private void OnDisable()
        {
            OnExperiencePickup -= ExperienceAddValue;
        }


        private void ExperienceAddValue(float value)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }
            _currentExperience += value;
            if (_currentExperience >= _experienceToUp)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            _currentExperience -= _experienceToUp;
            _currentLevel++;
            _upgradeWindow.SetActive(true);
            _upgradeWindow.GetComponent<UpgradeWindow>().GetRandomCards();
            switch (_currentLevel)
            {
                case <= 20:
                    _experienceToUp += 10;
                    break;
                case <= 40:
                    _experienceToUp += 13;
                    break;
                case <= 60:
                    _experienceToUp += 16;
                    break;
            }
        }
    }
}