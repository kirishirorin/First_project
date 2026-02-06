using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameCore.ExperienceSystem
{
    public class ExperienceUIUpdate : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _expText;
        [SerializeField] private Image _experienceBar;
        private ExperienceSystem _experienceSystem;


        private void OnEnable()
        {
            _experienceSystem.OnExperiencePickup += UpdateExperienceBar;
        }

        private void OnDisable()
        {
            _experienceSystem.OnExperiencePickup -= UpdateExperienceBar;
        }


        private void Start()
        {
            _experienceBar.fillAmount = 0f;
            _expText.text = "1 LVL";
        }

        private void UpdateExperienceBar(float experience)
        {
            _experienceBar.fillAmount =
                Mathf.Clamp01(_experienceSystem.CurrentExperience / _experienceSystem.ExperienceToUp);
            _expText.text = $"{_experienceSystem.CurrentLevel} LVL";
        }


        [Inject]
        private void Construct(ExperienceSystem experienceSystem)
        {
            _experienceSystem = experienceSystem;
        }
    }
}