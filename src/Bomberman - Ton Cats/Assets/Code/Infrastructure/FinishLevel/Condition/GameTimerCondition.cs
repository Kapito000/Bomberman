﻿using Gameplay.Feature.FinishLevel.Component;
using Infrastructure.ECS;
using Zenject;

namespace Infrastructure.FinishLevel.Condition
{
	public sealed class GameTimerCondition : IGameTimerCondition
	{
		[Inject] EntityWrapper _observer;

		public bool Check(int observerEntity)
		{
			_observer.SetEntity(observerEntity);
			return _observer.Has<GameTimerOver>();
		}
	}
}