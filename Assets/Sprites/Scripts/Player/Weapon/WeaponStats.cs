using UnityEngine;

namespace Sprites.Scripts.Player.Weapon
{
    public class WeaponStats : MonoBehaviour
    {
        [SerializeField] private float _speed, _damage, _range, _timeBetweenAttacks, _duration;

        public float Speed => _speed;
        public float Damage => _damage;
        public float Range => _range;
        public float TimeBetweenAttacks => _timeBetweenAttacks;
        public float Duration => _duration;
    }
}