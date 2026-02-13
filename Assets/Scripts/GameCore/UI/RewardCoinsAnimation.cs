using System.Collections;
using TMPro;
using UnityEngine;

namespace GameCore.UI
{
    [RequireComponent(typeof(AudioSource))]
    public class RewardCoinsAnimation: MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        private float _targetTimer;
        private const float AnimationDuration = 2.4f;


        public void ActivateAnimation(float targetValue, float currentValue, TMP_Text text)
        {
            StartCoroutine(Animate(targetValue, currentValue, text));
        }


        private IEnumerator Animate(float targetValue, float currentValue, TMP_Text text)
        {
            StartCoroutine(PichSound());
            float rate = Mathf.Abs(targetValue - currentValue) / AnimationDuration;
            while (Mathf.Abs(targetValue - currentValue) > 0.1f)
            {
                currentValue = Mathf.MoveTowards(currentValue, targetValue, rate * Time.deltaTime);
                text.text = Mathf.FloorToInt(currentValue).ToString();
                yield return null;
            }
        }

        private IEnumerator PichSound()
        {
            _targetTimer = 0;
            _audioSource.pitch = 1f;
            while (_targetTimer <= AnimationDuration)
            {
                _audioSource.Play();
                _audioSource.pitch +=  0.1f;
                _targetTimer += Time.deltaTime;
                yield return null;
            }
        }
    }
}