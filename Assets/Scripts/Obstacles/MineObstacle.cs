using Assets.Scripts.CharacterOptions;
using Assets.Scripts.GameHelpers;
using Assets.Scripts.ViewHelpers;
using UnityEngine;

namespace Assets.Scripts.Obstacles
{
    public class MineObstacle : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _effect;
        [SerializeField] private Transform _parent;

        private void OnValidate() => 
            _effect ??= GetComponentInChildren<ParticleSystem>();
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerCharacterActive player))
            {
                if (!player.GetIsDead)
                {
                    player.KillPlayer();

                    CameraShakeAnimation.Get.Shake();
                    EffectsSpawner.Get.InstantiateEffect(_effect, _parent);

                    DestroyGem();
                }
            }
        }

        private void DestroyGem() =>
            Destroy(gameObject);
    }
}
