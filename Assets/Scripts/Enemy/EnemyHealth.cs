using System.Collections;
using System.Threading.Tasks;
using GameCore.ExperienceSystem;
using GameCore.UI;
using GameCore.Health;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemyHealth : ObjectHealth
    {
        [SerializeField] private Transform _flash;
        private WaitForSeconds _tick = new WaitForSeconds(1f);
        private DamageTextSpawner _damageTextSpawner;
        private ExperienceSpawner  _experienceSpawner;
        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            _damageTextSpawner.Activate(transform, (int)damage);
            if (CurrentHealth <= 0)
            {
                gameObject.SetActive(false);
                ChanceToDropExperience();
            }
            else
            {
                Flash();
            }
        }

        public void Born(float damage)
        {
            StartCoroutine(StartBorn(damage));
        }

        private void ChanceToDropExperience()
        {
            if (Random.Range(0f, 100f) <= 33f)
            {
                _experienceSpawner.Spawn(transform.position);
            }
        }


        async private void Flash()
        {
            _flash.gameObject.SetActive(true);
            await Task.Delay(500);
            _flash.gameObject.SetActive(false);
        }

        private IEnumerator StartBorn(float damage)
        {
            if (gameObject.activeSelf == false)
            {
                yield break;
            }
            float tickDamage =  damage / 3f;
            if (tickDamage <= 1f)
            {
                tickDamage = 1f;
            }
            float roundDamage = Mathf.Round(tickDamage);
            for (int i = 0; i < 5; i++)
            {
                TakeDamage(roundDamage);
                yield return _tick;
            }
        }

        [Inject]
        private void Construct(DamageTextSpawner damageTextSpawner, ExperienceSpawner  experienceSpawner)
        {
            _damageTextSpawner =  damageTextSpawner;
            _experienceSpawner =  experienceSpawner;
        }
    }
}