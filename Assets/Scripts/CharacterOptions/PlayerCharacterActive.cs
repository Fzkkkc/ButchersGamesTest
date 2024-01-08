using System;
using UnityEngine;

namespace Assets.Scripts.CharacterOptions
{
    [RequireComponent(typeof(Animator))]
    public class PlayerCharacterActive : MonoBehaviour
    {
        public Action<PlayerCharacterActive> OnPlayerDeath;

        [SerializeField] private UnitRagdoll _unitRagdoll;
        [SerializeField] private Animator _playerAnimator;

        private bool _isDead;
        private int _keyHashAnimation;

        public bool GetIsDead => _isDead;

        private void OnValidate()
        {
            _playerAnimator ??= GetComponent<Animator>();
            _unitRagdoll ??= GetComponent<UnitRagdoll>();
        }

        private void Awake() =>
            GetHashKeyAnimation();

        public void SetMovePlayerState(bool isMove) => 
            _playerAnimator.SetBool(_keyHashAnimation, isMove);

        public void KillPlayer()
        {
            if (_isDead) return;

            _isDead = true;
            OnPlayerDeath?.Invoke(this);

            _unitRagdoll.ActivateRagdoll();

            Destroy(gameObject);
        }
        
        private void GetHashKeyAnimation() =>
            _keyHashAnimation = Animator.StringToHash("IsMove");

        public void SetVictoryState() => 
            _playerAnimator.SetTrigger($"Victory_{UnityEngine.Random.Range(0, 4)}");
    }
}
