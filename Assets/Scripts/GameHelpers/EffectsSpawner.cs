using UnityEngine;

namespace Assets.Scripts.GameHelpers
{
    public class EffectsSpawner : MonoBehaviour
    {
        private static EffectsSpawner _instance;
        
        public static EffectsSpawner Get => _instance;
        
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public void InstantiateEffect(ParticleSystem effect, Transform parentObject)
        {
            effect.gameObject.SetActive(true);
            effect.transform.parent = parentObject;
        }
    }
}