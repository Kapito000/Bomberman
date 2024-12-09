using Gameplay.UI.Factory;
using Leopotam.EcsLite;
using Zenject;

namespace Gameplay.UI.System
{
	public sealed class CreateRootCanvasSystem : IEcsRunSystem
	{
		[Inject] EcsWorld _world;
		[Inject] IUiFactory _uiFactory;
		
		public void Run(IEcsSystems systems)
		{
			_uiFactory.CreateRootCanvas();
			_uiFactory.EventSystem();
		}
	}
}