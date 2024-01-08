using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ViewHelpers
{
    public class CameraShakeAnimation : MonoBehaviour
    {
        private static CameraShakeAnimation _instance;
        private Coroutine _shakeRoutine;

        public static CameraShakeAnimation Get => _instance;

        private void Awake()
        {
            if (!_instance) _instance = this;
        }

        public void Shake()
        {
            if (_shakeRoutine != null)
            {
                StopCoroutine(_shakeRoutine);
            }

            _shakeRoutine = StartCoroutine(ShakeRoutineAnimation());
        }

        private IEnumerator ShakeRoutineAnimation()
        {
            Vector3 _startPosition = transform.position;
            float _elapsed = 0f;

            while (_elapsed < 0.2f)
            {
                float _x = Random.Range(-0.13f, 0.13f);
                float _y = Random.Range(-0.13f, 0.13f);

                transform.position = new Vector3(transform.position.x + _x, transform.position.y + _y, transform.position.z);

                _elapsed += Time.deltaTime;

                yield return null;
            }

            transform.position = _startPosition;
            _shakeRoutine = null;
        }
    }
}