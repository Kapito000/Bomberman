using Input;
using Zenject;

namespace Gameplay.Input.Character
{
	public sealed class CommonInput : IInput
	{
		[Inject] Controls _controls;
		
		public void Enable()
		{
			_controls.Enable();
		}

		public void Disable()
		{
			_controls.Disable();
		}
	}
}