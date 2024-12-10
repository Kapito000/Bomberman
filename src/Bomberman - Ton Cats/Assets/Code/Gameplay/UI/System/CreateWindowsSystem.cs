using System.Collections.Generic;
using Common.Component;
using Gameplay.UI.Component;
using Gameplay.UI.StaticData;
using Gameplay.Windows;
using Infrastructure.AssetProvider;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;

namespace Gameplay.UI.System
{
	public sealed class CreateWindowsSystem : IEcsRunSystem
	{
		[Inject] WindowKitId _windowKitId;
		[Inject] EntityWrapper _root;
		[Inject] IWindowService _windowService;
		[Inject] IAssetProvider _assetProvider;
		[Inject] IWindowKitData _windowKitData;

		readonly EcsFilterInject<
			Inc<WindowsRoot, TransformComponent>> _windowsRootFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _windowsRootFilter.Value)
			{
				_root.SetEntity(e);
				var parent = _root.Transform();
				if (TryGetWindowsIdsList(out var windowsIds) == false)
				{
					Debug.LogError("Cannot to create windows.");
					return;
				}
				_windowService.Create(parent, windowsIds);
			}
		}

		bool TryGetWindowsIdsList(out IReadOnlyList<WindowId> list) =>
			_windowKitData.TryGetKit(_windowKitId, out list);
	}
}