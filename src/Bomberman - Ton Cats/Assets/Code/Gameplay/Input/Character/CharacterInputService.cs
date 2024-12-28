using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Gameplay.Input.Character
{
	public sealed class CharacterInputService : ICharacterInput, IDisposable
	{
		[Inject] Controls _controls;

		public Vector2 Movement { get; private set; }
		public event Action PutBomb;
		public event Action<Vector2> ScreenClick;

		[Inject]
		void Construct()
		{
			_controls.Character.PutBomb.performed += OnPutBombPerformed;
			_controls.Character.Movement.performed += OnMovePerformed;
			_controls.Character.ScreenTap.performed += OnScreenTapPerformed;
		}

		public void Enable() =>
			_controls.Character.Enable();

		public void Disable() =>
			_controls.Character.Disable();

		public void Dispose()
		{
			_controls.Character.PutBomb.performed -= OnPutBombPerformed;
			_controls.Character.Movement.performed -= OnMovePerformed;
			_controls.Character.ScreenTap.performed -= OnScreenTapPerformed;
		}

		void OnPutBombPerformed(InputAction.CallbackContext context) =>
			PutBomb?.Invoke();

		void OnMovePerformed(InputAction.CallbackContext context)
		{
			Movement = context.ReadValue<Vector2>();
		}

		void OnScreenTapPerformed(InputAction.CallbackContext context)
		{
			var screenPos = _controls.Character.MousePos.ReadValue<Vector2>();
			ScreenClick?.Invoke(screenPos);
		}
	}
}