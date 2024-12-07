﻿using Common.Component;
using Gameplay.Feature.UI.Component;
using Gameplay.Feature.UI.Factory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.UI.System
{
	public sealed class CreateWindowsRootSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _uiRoot;

		[Inject] IUiFactory _uiFactory;

		readonly EcsFilterInject<Inc<UiRoot, TransformComponent>> _uiRootFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _uiRootFilter.Value)
			{
				_uiRoot.SetEntity(e);
				var parent = _uiRoot.Transform();
				_uiFactory.WindowsRoot(parent);
			}
		}
	}
}