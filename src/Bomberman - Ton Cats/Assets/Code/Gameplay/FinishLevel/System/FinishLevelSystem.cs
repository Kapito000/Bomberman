using Infrastructure.FinishLevel;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Gameplay.FinishLevel.System
{
	public sealed class FinishLevelSystem : IEcsRunSystem
	{
		[Inject] IFinishLevelService _finishLevelServiceService;

		public void Run(IEcsSystems systems)
		{
			if (_finishLevelServiceService.CanFinishLevel == false)
				return;

			Debug.Log("Process the finish level.");
		}
	}
}