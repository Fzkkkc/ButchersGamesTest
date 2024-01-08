using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Obstacles
{
    [RequireComponent(typeof(Animator))]
    public class ConeObstacle : MonoBehaviour
    {
        [SerializeField] private Animator _coneAnimator;

        private int _keyHashAnimation;

        private void OnValidate() =>
            _coneAnimator ??= GetComponent<Animator>();

        private void Start()
        {
            GetKeyHash();

            StartCoroutine(ShowRoutine());
        }

        private IEnumerator ShowRoutine()
        {
            WaitForSeconds hashSecond = new WaitForSeconds(Random.Range(2, 5f));

            while (true)
            {
                yield return hashSecond;

                PlayConeAnimation();
            }
        }
        
        private void GetKeyHash() =>
            _keyHashAnimation = Animator.StringToHash("Show");
        
        private void PlayConeAnimation() =>
            _coneAnimator.SetTrigger(_keyHashAnimation);
    }
}
