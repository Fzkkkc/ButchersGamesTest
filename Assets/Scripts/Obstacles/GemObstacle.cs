using Assets.Scripts.CharacterOptions;
using Assets.Scripts.GameHelpers;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Obstacles
{
    public class GemObstacle : MonoBehaviour
    {
        [SerializeField] private GemCounter _gemCurrency;
        [SerializeField] private ParticleSystem _effect;
        [SerializeField] private Transform _parent;

        private void OnValidate() => 
            _effect ??= GetComponentInChildren<ParticleSystem>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerCharacterActive player))
            {
                _gemCurrency.IncreaseGemCount(1);
                EffectsSpawner.Get.InstantiateEffect(_effect, _parent);

                DestroyGem();
            }
        }

        private void DestroyGem() =>
            Destroy(gameObject);
    }
}
