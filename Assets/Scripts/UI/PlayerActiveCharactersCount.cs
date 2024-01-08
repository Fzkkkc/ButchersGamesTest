using Assets.Scripts.CharacterOptions;
using Assets.Scripts.ViewHelpers;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class PlayerActiveCharactersCount : MonoBehaviour
    {
        [SerializeField] private PoolActivePlayerCharacters _poolActivePlayerCharacters;
        [SerializeField] private PlayerActiveCharactersCountText _charactersCountText;
        [SerializeField] private ImpulseUiGameObject _impulseUiGameObject;
        
        private readonly Color _decreasedActiveCharactersTextColor = Color.red;
        private readonly Color _increasedActiveCharactersTextColor = Color.green;

        private int _countActivePlayerCharacters;
        private Color _defaultTextColor;
        
        private void OnValidate()
        {
            _impulseUiGameObject ??= GetComponentInChildren<ImpulseUiGameObject>();
            _charactersCountText ??= GetComponentInChildren<PlayerActiveCharactersCountText>();
        }
        
        public int ActiveCharactersCount
        {
            get => _countActivePlayerCharacters;
            private set => _countActivePlayerCharacters = value;
        }

        private void Start()
        {
            _poolActivePlayerCharacters.OnPlayerCharacterRemoved += DecreasedCharactersCount;
            _poolActivePlayerCharacters.OnPlayerCharacterAdded += IncreasedCharactersCount;

            _defaultTextColor = _charactersCountText.ActiveCharactersTextColor;

            InitializeCharactersCounter(_poolActivePlayerCharacters.GetActiveCharactersList.Count);
        }

        private void OnDestroy()
        {
            _poolActivePlayerCharacters.OnPlayerCharacterRemoved -= DecreasedCharactersCount;
            _poolActivePlayerCharacters.OnPlayerCharacterAdded -= IncreasedCharactersCount;
        }
        
        private void InitializeCharactersCounter(int count)
        {
            ActiveCharactersCount = count;
            _charactersCountText.UpdateText();
        }

        private void IncreasedCharactersCount(int count)
        {
            ActiveCharactersCount = count;
            _charactersCountText.UpdateText();
            
            _charactersCountText.SetActiveCharactersCountTextColor(_increasedActiveCharactersTextColor);
            _impulseUiGameObject.Pulse(() =>
            {
                _charactersCountText.SetActiveCharactersCountTextColor(_defaultTextColor);
            });
        }

        private void DecreasedCharactersCount(int count)
        {
            ActiveCharactersCount = count;

            if(ActiveCharactersCount <= 0)
            {
                ActiveCharactersCount = 0;
            }

            _charactersCountText.UpdateText();

            _charactersCountText.SetActiveCharactersCountTextColor(_decreasedActiveCharactersTextColor);
            _impulseUiGameObject.Pulse(() =>
            {
                _charactersCountText.SetActiveCharactersCountTextColor(_defaultTextColor);
            });
        }
    }
}
