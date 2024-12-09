using Gameplay.Windows;
using Leopotam.EcsLite;
using Zenject;

namespace Gameplay.Feature.MainMenu.System
{
	public sealed class InitManiMenuWindowSystem : IEcsRunSystem
	{
		[Inject] IWindowService _windowService;

		public void Run(IEcsSystems systems)
		{
			_windowService.Open(WindowId.MainMenu);
		}
	}
}