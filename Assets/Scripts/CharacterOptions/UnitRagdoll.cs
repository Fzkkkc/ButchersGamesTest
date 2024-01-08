using UnityEngine;

namespace Assets.Scripts.CharacterOptions
{
    public class UnitRagdoll : MonoBehaviour
    {
        [SerializeField] private GameObject _ragdoll;

        public void ActivateRagdoll()
        {
            Instantiate(_ragdoll, transform.position, Quaternion.identity);
        }
    }
}
