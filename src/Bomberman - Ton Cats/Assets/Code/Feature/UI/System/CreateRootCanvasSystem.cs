using Feature.UI.Factory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Zenject;

namespace Feature.UI.System
{
	public sealed class CreateRootCanvasSystem : EcsSystem, IEcsRunSystem
	{
		[Inject] IUiFactory _uiFactory;
		
		public void Run(IEcsSystems systems)
		{
			_uiFactory.CreateRootCanvas();
			_uiFactory.EventSystem();
		}
	}
}