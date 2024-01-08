using UnityEngine;

namespace Assets.Scripts.GameHelpers
{
    public class RotateSaw : MonoBehaviour
    {
        private float _speed = 150f;
        private readonly Vector3 _rotateVector = Vector3.back;

        private void Update()
        {
            transform.Rotate(_rotateVector, _speed * Time.deltaTime);
        }
    }
}
