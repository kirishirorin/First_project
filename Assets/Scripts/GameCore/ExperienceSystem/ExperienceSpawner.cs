using GameCore.Pool;
using UnityEngine;
using Zenject;

namespace GameCore.ExperienceSystem
{
    public class ExperienceSpawner : MonoBehaviour
    {
        [SerializeField] private ObjectPool _objectPool;


        public void Spawn(Vector3 position)
        {
            var experience = _objectPool.GetFromPool();
            experience.transform.SetParent(transform);
            experience.transform.position = position;
        }
    }
}