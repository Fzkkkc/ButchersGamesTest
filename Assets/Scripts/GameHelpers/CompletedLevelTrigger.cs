using System;
using System.Collections.Generic;
using Assets.Scripts.CharacterOptions;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.GameHelpers
{
    public class CompletedLevelTrigger : MonoBehaviour
    {
        public Action OnLevelCompleted;

        [SerializeField] private Transform[] _points;
        [SerializeField] private PoolActivePlayerCharacters _poolActivePlayerCharacters;

        private List<PointFinish> _charactersPointPositionOnFinish = new List<PointFinish>();
        private bool _isCompleted;

        private void Start()
        {
            foreach (var pointTransform in _points)
            {
                PointFinish point = new PointFinish {Transform = pointTransform.transform, IsOccupied = false};

                _charactersPointPositionOnFinish.Add(point);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out PlayerCharacterActive player)) return;
            if (_isCompleted) return;

            _isCompleted = true;
            OnLevelCompleted?.Invoke();

            foreach (var activeCharacter in _poolActivePlayerCharacters.GetActiveCharactersList)
            {
                int pointIndex = 0;
                bool isOccupied = false;

                while (!isOccupied)
                {
                    if (!_charactersPointPositionOnFinish[pointIndex].IsOccupied)
                    {
                        activeCharacter.transform.DOMove(_charactersPointPositionOnFinish[pointIndex].Transform.position, 0.3f);
                        activeCharacter.transform.DORotate(new Vector3(0f, -180f, 0f), 0.3f);

                        _charactersPointPositionOnFinish[pointIndex].IsOccupied = true;

                        isOccupied = true;
                    }

                    pointIndex++;

                    if(pointIndex >= _charactersPointPositionOnFinish.Count)
                    {
                        pointIndex = 0;
                    }
                }
            }
        }
    }
}