using Infrastructure;

namespace Input.Character
{
	public interface IInput : IService
	{
		void Enable();
		void Disable();
	}
}