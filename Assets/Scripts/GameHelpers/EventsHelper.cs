using Assets.Scripts.CharacterOptions;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.GameHelpers
{
    public class EventsHelper : MonoBehaviour
    {
        [SerializeField] private PoolActivePlayerCharacters _poolActivePlayerCharacters;
        [SerializeField] private LevelMovementSystem _levelMovementSystem;
        [SerializeField] private GameObject _losePopup;
        [SerializeField] private GameObject _victoryPopup;
        [SerializeField] private Animator _canvasAnimator;
        [SerializeField] private CompletedLevelTrigger _completedLevelTrigger;
        [SerializeField] private ParticleSystem[] _finishEffects;
        [SerializeField] private Camera _mainCamera;

        private bool _endGame;
        private int _hashAnimationKey;

        private void Start()
        {
            AddListeners();

            GetKeyAnimationKey();
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }

        private void StartMove()
        {
            _levelMovementSystem.IsMove = true;

            foreach (var activeCharacter in _poolActivePlayerCharacters.GetActiveCharactersList)
            {
                activeCharacter.SetMovePlayerState(true);
            }
        }

        private void LoseGame()
        {
            if (_endGame) return;

            _endGame = true;

            _losePopup.SetActive(true);
            _canvasAnimator.SetTrigger(_hashAnimationKey);
            _levelMovementSystem.IsMove = false;
        }

        private void VictoryGame()
        {
            if (_endGame) return;

            _endGame = true;

            _victoryPopup.SetActive(true);
            _canvasAnimator.SetTrigger(_hashAnimationKey);
            _levelMovementSystem.IsMove = false;

            foreach (var activeCharacter in _poolActivePlayerCharacters.GetActiveCharactersList)
            {
                activeCharacter.SetMovePlayerState(false);
                activeCharacter.SetVictoryState();
            }

            foreach (var activeCharacter in _finishEffects)
            {
                activeCharacter.Play();
            }

            _mainCamera.transform.DOMove(new Vector3(_mainCamera.transform.position.x, _mainCamera.transform.position.y, -9), 0.5f);
        }

        private void AddListeners()
        {
            _poolActivePlayerCharacters.OnFirstDrawing += StartMove;
            _poolActivePlayerCharacters.OnAllPlayerCharactersRemoved += LoseGame;
            _completedLevelTrigger.OnLevelCompleted += VictoryGame;
        }

        private void RemoveListeners()
        {
            _poolActivePlayerCharacters.OnFirstDrawing -= StartMove;
            _poolActivePlayerCharacters.OnAllPlayerCharactersRemoved -= LoseGame;
            _completedLevelTrigger.OnLevelCompleted -= VictoryGame;
        }

        private void GetKeyAnimationKey()
        {
            _hashAnimationKey = Animator.StringToHash("Disable");
        }
    }
}
