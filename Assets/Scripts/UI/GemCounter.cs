using Assets.Scripts.ViewHelpers;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class GemCounter : MonoBehaviour
    {
        [SerializeField] private ImpulseUiGameObject _impulseUiGameObject;
        [SerializeField] private GemCounterText _gemCounterText;
        
        private readonly Color _gemCountIncreasedTextColor = Color.green;

        private int _gemCount;
        private Color _defaultTextColor;

        public int GemCount
        {
            get => _gemCount;
            private set => _gemCount = value;
        } 
        
        private void OnValidate()
        {
            _impulseUiGameObject ??= GetComponentInChildren<ImpulseUiGameObject>();
            _gemCounterText ??= GetComponentInChildren<GemCounterText>();
        }

        private void Start()
        {
            GemCount = 0;
            _defaultTextColor = _gemCounterText.GemColor;

            InitializeGemCount(_gemCount);
        }

        private void InitializeGemCount(int count)
        {
            _gemCount = count;
            _gemCounterText.UpdateText();
        }

        public void IncreaseGemCount(int count)
        {
            _gemCount += count;
            _gemCounterText.UpdateText();

            _gemCounterText.SetGemColor(_gemCountIncreasedTextColor);
            _impulseUiGameObject.Pulse(() =>
            {
                _gemCounterText.SetGemColor(_defaultTextColor);
            });
        }
    }
}
