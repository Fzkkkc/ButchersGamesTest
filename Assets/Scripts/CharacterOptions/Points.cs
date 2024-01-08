using UnityEngine;

namespace Assets.Scripts.CharacterOptions
{
    public class Points: MonoBehaviour
    {
        public Vector3 PointPosition;
        public bool IsAdded;
        public GameObject PointGameObject;

        public Points(Vector3 pos, GameObject point)
        {
            PointPosition = pos;
            IsAdded = false;
        }
    }
}