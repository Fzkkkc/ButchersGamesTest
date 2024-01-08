using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class PlayerActiveCharactersCountText : MonoBehaviour
    {
        [SerializeField] private Text _countActiveCharactersText;
        [SerializeField] private PlayerActiveCharactersCount _playerActiveCharactersCount;

        private void OnValidate()
        {
            _countActiveCharactersText ??= GetComponent<Text>();
            _playerActiveCharactersCount ??= GetComponentInParent<PlayerActiveCharactersCount>();
        }
        
        public Color ActiveCharactersTextColor
        {
            get => _countActiveCharactersText.color;
            private set => _countActiveCharactersText.color = value;
        }

        public void SetActiveCharactersCountTextColor(Color color)
        {
            ActiveCharactersTextColor = color;
        }

        public void UpdateText()
        {
            _countActiveCharactersText.text = _playerActiveCharactersCount.ActiveCharactersCount.ToString();
        }
    }
}