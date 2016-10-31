using UnityEngine;
using GameProgramming3D.Messages;
using DG.Tweening;
using System.Collections;
using System;

namespace GameProgramming3D
{
	public class GameCamera : MonoBehaviour
	{
		[SerializeField] private Transform _cameraTarget;
		[SerializeField] private float _moveSpeed;
		[SerializeField] private float _rotateSpeed;
		[SerializeField] private float _maxZoom;
		[SerializeField] private float _minZoom;
		[SerializeField] private int _zoomSteps;

		private Camera _camera;
		private float _zoomStep;

		private ISubscription<MoveCameraMessage> _moveCameraSubscription;
		private ISubscription<ZoomCameraMessage> _zoomCameraSubscription;
		private ISubscription<RotateCameraMessage> _rotateCameraSubscription;

		protected void Awake()
		{
			_zoomStep = ( _minZoom - _maxZoom ) / _zoomSteps;
			_camera = GetComponentInChildren<Camera> ();
		}

		protected void OnEnable()
		{
			_moveCameraSubscription = 
				GameManager.Instance.MessageBus
				.Subscribe<MoveCameraMessage> ( MoveCamera );

			_zoomCameraSubscription =
				GameManager.Instance.MessageBus
				.Subscribe<ZoomCameraMessage> ( Zoom );

			_rotateCameraSubscription =
				GameManager.Instance.MessageBus
				.Subscribe<RotateCameraMessage> ( Rotate );
		}


		protected void OnDisable()
		{
			GameManager.Instance.MessageBus.Unsubscribe ( _moveCameraSubscription );
			GameManager.Instance.MessageBus.Unsubscribe ( _zoomCameraSubscription );
			GameManager.Instance.MessageBus.Unsubscribe ( _rotateCameraSubscription );
		}

		private void MoveCamera ( MoveCameraMessage message )
		{
			Vector2 direction = message.Amount;
			direction *= _moveSpeed * Time.deltaTime;
			_cameraTarget.Translate ( new Vector3 ( direction.x, 0, direction.y ) );
		}

		private void Rotate ( RotateCameraMessage message )
		{
			Vector3 rotation = _cameraTarget.localEulerAngles;
			rotation.y += message.Amount * _rotateSpeed * Time.deltaTime;
			_cameraTarget.localEulerAngles = rotation;
		}

		private void Zoom ( ZoomCameraMessage message )
		{
			int zoomFactor = message.Direction == ZoomCameraMessage.ZoomDirection.In
				? -1
				: 1;
			float zoomAmount = _camera.transform.localPosition.y;
			zoomAmount =
				Mathf.Clamp ( zoomAmount + ( _zoomStep * zoomFactor ), 
				_maxZoom, _minZoom );
			Vector3 position = _camera.transform.localPosition;
			position.y = zoomAmount;
			//_camera.transform.localPosition = position;
			var tweener = DOTween.To (
				() => _camera.transform.localPosition,
				( value ) => _camera.transform.localPosition = value,
				position, 0.5f );
		}
	}
}
