//-----------------------------------------------------------------------
// File name: ObjectToMousePosition.cs
// Author: Dayron Suárez del Toro
// Email: dsuarezt92@gmail.com
// Created on: April 23, 2023
//-----------------------------------------------------------------------

using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace LitLab.CyberTitans.Level
{
    public class ObjectToMousePosition
    {
        #region Fields

        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken;
        private GameObject _gameObject;
        private Camera _mainCamera;
        private LayerMask _layerMask;
        private Vector3 _mousePosition;
        private Ray _ray;
        private RaycastHit _hit;
        private Vector3 _hitPoint;

        #endregion

        #region Constructors

        public ObjectToMousePosition(LayerMask layerMask)
        {
            _layerMask = layerMask;
            _mainCamera = Camera.main;
        }

        #endregion

        #region Methods

        public void FollowMouse(GameObject gameObject)
        {
            _gameObject = gameObject;
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            FollowMouseAsync().Forget();
        }

        public void Cancel()
        {
            _gameObject = null;
            _cancellationTokenSource.Cancel();
        }

        private async UniTask FollowMouseAsync()
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                _gameObject.transform.position = GetHitPoint();

                await UniTask.Yield(PlayerLoopTiming.Update);
            }
        }

        private Vector3 GetHitPoint()
        {
            _hitPoint = Vector3.zero;
            _mousePosition = Input.mousePosition;
            _mousePosition.z = 100f;
            _ray = _mainCamera.ScreenPointToRay(_mousePosition);

            if (Physics.Raycast(_ray, out _hit, 100f, _layerMask))
            {
                _hitPoint = _hit.point;
            }

            return _hitPoint;
        }

        #endregion
    }
}
