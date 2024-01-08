using Assets.Scripts.CharacterOptions;
using UnityEngine;

namespace Assets.Scripts.Obstacles
{
    public class DeathCollider : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerCharacterActive player))
            {
                if (!player.GetIsDead)
                {
                    player.KillPlayer();
                }
            }
        }
    }
}
