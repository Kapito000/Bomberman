using Factory.UiFactory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Zenject;

namespace Feature.UI.System
{
	public sealed class CreateRootCanvasSystem : EcsSystem, IEcsInitSystem
	{
		[Inject] IUiFactory _uiFactory;
		
		public void Init(IEcsSystems systems)
		{
			_uiFactory.CreateRootCanvas();
			_uiFactory.EventSystem();
		}
	}
}