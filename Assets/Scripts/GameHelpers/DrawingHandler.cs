using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.GameHelpers
{
    public class DrawingHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public Action<LineRenderer> OnEndDrawing;

        [SerializeField] private LineRenderer _brushRenderer;
        [SerializeField] private Camera _mainCamera;
        
        private readonly Vector2 _limitPositionX = new Vector2(-200f, 200f);
        private readonly Vector2 _limitPositionY = new Vector2(-200f, 200f);

        private LineRenderer _currentBrush;
        private Vector3 _lastDrawnPosition;

        private Ray _ray;
        private RaycastHit _hit;

        private void DestroyBrush()
        {
            if (_currentBrush != null) Destroy(_currentBrush.gameObject);
            _currentBrush = null;
        }

        private void CreateBrush(Vector3 initialPosition)
        {
            if (_currentBrush != null) DestroyBrush();

            _currentBrush = Instantiate(_brushRenderer);
            _currentBrush.transform.parent = transform;

            _currentBrush.SetPosition(0, initialPosition);
            _currentBrush.SetPosition(1, initialPosition);
        }

        private void AddPoint(Vector3 pointPos)
        {
            _currentBrush.positionCount++;
            int posIndex = _currentBrush.positionCount - 1;
            _currentBrush.SetPosition(posIndex, pointPos);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(_ray, out _hit, 1000);

            if (_hit.collider != null)
            {
                Vector3 mousePos = new Vector3(_hit.point.x, _hit.point.y + 0.4f, _hit.point.z);

                if (mousePos != _lastDrawnPosition && Vector3.Distance(mousePos, _lastDrawnPosition) >= 0.2f)
                {
                    AddPoint(mousePos);
                    _lastDrawnPosition = mousePos;
                }
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnEndDrawing?.Invoke(_currentBrush);

            DestroyBrush();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(_ray, out _hit, 1000);

            if (_hit.collider != null)
            {
                Vector3 mousePos = new Vector3(_hit.point.x, _hit.point.y + 0.4f, _hit.point.z);

                if (mousePos.x > _limitPositionX.x && mousePos.x < _limitPositionX.y && mousePos.y > _limitPositionY.x && mousePos.y < _limitPositionY.y)
                {
                    CreateBrush(mousePos);
                }
            }
        }
    }
}
