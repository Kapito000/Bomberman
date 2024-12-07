﻿using Gameplay.Feature.FinishLevel.Component;
using Gameplay.Windows;
using Infrastructure.FinishLevel;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.FinishLevel.System
{
	public sealed class FinishLevelSystem : IEcsRunSystem
	{
		[Inject] IWindowService _windowService;
		[Inject] IFinishLevelService _finishLevelService;

		readonly EcsFilterInject<Inc<FinishLevelObserver>> _finishLevelObserver;

		public void Run(IEcsSystems systems)
		{
			foreach (var observerEntity in _finishLevelObserver.Value)
			{
				if (_finishLevelService.GameOver(observerEntity))
				{
					_finishLevelService.LaunchGamePause();
					_windowService.Open(WindowId.GameOver);
					continue;
				}

				if (_finishLevelService.LevelComplete(observerEntity))
				{
					_finishLevelService.LaunchGamePause();
					_windowService.Open(WindowId.LevelComplete);
					continue;
				}
			}
		}
	}
}