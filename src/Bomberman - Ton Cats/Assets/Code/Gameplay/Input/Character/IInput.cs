using Infrastructure;

namespace Gameplay.Input.Character
{
	public interface IInput : IService
	{
		void Enable();
		void Disable();
	}
}