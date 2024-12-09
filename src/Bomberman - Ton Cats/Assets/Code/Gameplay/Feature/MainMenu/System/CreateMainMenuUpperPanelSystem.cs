using Common.Component;
using Gameplay.Feature.GameUI.Component;
using Gameplay.Feature.MainMenu.Factory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.MainMenu.System
{
	public sealed class CreateMainMenuUpperPanelSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _root;

		[Inject] IMainMenuFactory _menuFactory;

		readonly EcsFilterInject<Inc<UiRoot, TransformComponent>> _uiRootFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _uiRootFilter.Value)
			{
				_root.SetEntity(e);
				var parent = _root.Transform();
				_menuFactory.CreateUpperPanel(parent);
			}
		}
	}
}