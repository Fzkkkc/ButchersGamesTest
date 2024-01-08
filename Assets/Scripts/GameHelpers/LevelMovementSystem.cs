using UnityEngine;

namespace Assets.Scripts.GameHelpers
{
    public class LevelMovementSystem : MonoBehaviour
    {
        [SerializeField] private Transform _level;

        private bool _isMove;

        public bool IsMove { get => _isMove; set => _isMove = value; }

        private void Update()
        {
            if (!_isMove) return;
            Move();
        }

        private void Move()
        {
            _level.transform.position = Vector3.Lerp(_level.transform.position,
                new Vector3(_level.transform.position.x, _level.transform.position.y, _level.position.z - 15), 1f * Time.deltaTime);
        }
    }
}
