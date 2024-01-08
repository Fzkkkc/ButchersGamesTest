using Assets.Scripts.CharacterOptions;
using Assets.Scripts.GameHelpers;
using UnityEngine;

namespace Assets.Scripts.Obstacles
{
    public class IdlePlayerCharacter : MonoBehaviour
    {
        [SerializeField] private PoolActivePlayerCharacters _poolActivePlayerCharacters;
        [SerializeField] private ParticleSystem _effect;
        [SerializeField] private Transform _parent;

        private bool _isActive;

        private void OnValidate() => 
            _effect ??= GetComponentInChildren<ParticleSystem>();
        
        private void ActivateIdleCharacter(PlayerCharacterActive playerCharacterActiveCharacter)
        {
            if (_isActive) return;

            _isActive = true;

            EffectsSpawner.Get.InstantiateEffect(_effect, _parent);

            PlayerCharacterActive playerCharacterActive = Instantiate(playerCharacterActiveCharacter, transform.position, Quaternion.identity);

            playerCharacterActive.SetMovePlayerState(true);
            _poolActivePlayerCharacters.Add(playerCharacterActive);
            playerCharacterActive.transform.parent = _poolActivePlayerCharacters.transform;

            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerCharacterActive player))
            {
                ActivateIdleCharacter(player);
            }
        }
    }
}
