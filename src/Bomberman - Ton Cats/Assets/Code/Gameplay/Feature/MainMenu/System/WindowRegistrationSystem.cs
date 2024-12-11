using Gameplay.Windows;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.MainMenu.System
{
	public sealed class WindowRegistrationSystem : IEcsRunSystem
	{
		[Inject] IWindowService _windowService;

		public void Run(IEcsSystems systems)
		{
			var windows = FindAllWindows();
			foreach (var window in windows)
			{
				if (_windowService.TryRegistry(window) == false)
				{
					Debug.LogError($"Cannot to registry window \"{window.Id}\".");
					continue;
				}
			}
		}

		static BaseWindow[] FindAllWindows() =>
			Object.FindObjectsByType<BaseWindow>(FindObjectsInactive.Include,
				FindObjectsSortMode.None);
	}
}