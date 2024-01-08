using UnityEngine;

namespace Assets.Scripts.CharacterOptions
{
    public class Ragdoll : MonoBehaviour
    {
        private void Update()
        {
            Move();
        }

        private void Move()
        {
            transform.position = Vector3.Lerp(transform.position,
                new Vector3(transform.position.x, transform.position.y, transform.position.z - 15), 1f * Time.deltaTime);
        }
    }
}
