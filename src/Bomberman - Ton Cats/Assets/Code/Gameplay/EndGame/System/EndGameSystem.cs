using Infrastructure.FinishLevel;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Gameplay.EndGame.System
{
	public sealed class EndGameSystem : IEcsRunSystem
	{
		[Inject] IFinishLevelService _finishLevelServiceService;

		public void Run(IEcsSystems systems)
		{
			if (_finishLevelServiceService.CanFinishLevel)
			{
				Debug.Log("Process the finish level.");
			}
		}
	}
}