using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.GameHelpers;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.CharacterOptions
{
    public class PoolActivePlayerCharacters : MonoBehaviour
    {
        public Action OnFirstDrawing;
        public Action<int> OnPlayerCharacterRemoved;
        public Action<int> OnPlayerCharacterAdded;
        public Action OnAllPlayerCharactersRemoved;

        [SerializeField] private List<PlayerCharacterActive> _activeCharactersList;
        [SerializeField] private DrawingHandler _drawingHandler;
        [SerializeField] private GameObject _point;

        private List<Points> _pointsList = new List<Points>();
        private bool _isFirstPlayerDraw;
        private int _countActivePlayerCharacters;

        public List<PlayerCharacterActive> GetActiveCharactersList => _activeCharactersList;

        private void Start()
        {
            _drawingHandler.OnEndDrawing += SetPosition;
            _countActivePlayerCharacters = _activeCharactersList.Count;

            foreach (var activeCharacter in _activeCharactersList)
            {
                activeCharacter.OnPlayerDeath += RemovePlayer;
            }
        }

        private void OnDestroy()
        {
            _drawingHandler.OnEndDrawing -= SetPosition;

            foreach (var activeCharacter in _activeCharactersList)
            {
                activeCharacter.OnPlayerDeath -= RemovePlayer;
            }
        }

        private void RemovePlayer(PlayerCharacterActive playerCharacterActive)
        {
            _activeCharactersList.Remove(playerCharacterActive);
            _countActivePlayerCharacters--;

            OnPlayerCharacterRemoved?.Invoke(_countActivePlayerCharacters);

            if(_countActivePlayerCharacters <= 0)
            {
                OnAllPlayerCharactersRemoved?.Invoke();
            }
        }

        public void Add(PlayerCharacterActive playerCharacterActive)
        {
            _activeCharactersList.Add(playerCharacterActive);
            _countActivePlayerCharacters++;
            playerCharacterActive.OnPlayerDeath += RemovePlayer;

            OnPlayerCharacterAdded?.Invoke(_countActivePlayerCharacters);
        }

        private void DestroyPoints()
        {
            foreach (var point in _pointsList.Where(point => point.PointGameObject != null))
            {
                Destroy(point.PointGameObject);
            }

            _pointsList = new List<Points>();
        }

        private void InitPoints(LineRenderer line)
        {
            if (!_isFirstPlayerDraw)
            {
                _isFirstPlayerDraw = true;
                OnFirstDrawing?.Invoke();
            }

            DestroyPoints();

            int playerLine = line.positionCount / _activeCharactersList.Count;
            int number = 0;

            if (playerLine == 0)
            {
                playerLine = 1;
            }

            for (int i = 0; i < line.positionCount; i++)
            {
                if (i == number * (playerLine + 1))
                {
                    Points point = new Points(line.GetPosition(i), _point);
                    _pointsList.Add(point);

                    number++;
                }
            }
        }

        private void SetPosition(LineRenderer line)
        {
            InitPoints(line);

            int indexPointLine = 0;
            int countAddedPlayers = 0;

            while (countAddedPlayers < _activeCharactersList.Count)
            {
                if (indexPointLine >= _pointsList.Count)
                {
                    indexPointLine = 0;
                }

                if (!_pointsList[indexPointLine].IsAdded)
                {
                    if (_pointsList.Count >= _activeCharactersList.Count)
                    {
                        _pointsList[indexPointLine].IsAdded = true;
                    }

                    Vector3 pos = new Vector3(_pointsList[indexPointLine].PointPosition.x, 0.2f/*0.505f*/, _pointsList[indexPointLine].PointPosition.z + 2);
                    _activeCharactersList[countAddedPlayers].transform.DOMove(pos, 0.2f);

                    countAddedPlayers++;
                }

                indexPointLine++;
            }
        }
    }
}