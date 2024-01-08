using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(Text))]
    public class GemCounterText : MonoBehaviour
    {
        [SerializeField] private Text _gemCount;
        [SerializeField] private GemCounter _gemCounter;

        private void OnValidate()
        {
            _gemCount ??= GetComponent<Text>();
            _gemCounter ??= GetComponentInParent<GemCounter>();
        }
        
        public Color GemColor
        {
            get => _gemCount.color;
            private set => _gemCount.color = value;
        }

        public void SetGemColor(Color color)
        {
           GemColor = color;
        }

        public void UpdateText()
        {
            _gemCount.text = _gemCounter.GemCount.ToString();
        }
    }
}