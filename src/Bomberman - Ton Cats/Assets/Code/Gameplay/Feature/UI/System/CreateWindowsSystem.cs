using Common.Component;
using Gameplay.Feature.UI.Component;
using Gameplay.Windows;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.UI.System
{
	public sealed class CreateWindowsSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _root;
		[Inject] IWindowService _windowService;

		readonly EcsFilterInject<
			Inc<WindowsRoot, TransformComponent>> _windowsRootFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _windowsRootFilter.Value)
			{
				_root.SetEntity(e);
				var parent = _root.Transform();
				var windowsIds = WindowsIdsList();
				_windowService.Create(parent, windowsIds);
			}
		}

		WindowId[] WindowsIdsList()
		{
			return new[]
			{
				WindowId.GameOver,
				// WindowId.LevelComplete
			};
		}
	}
}