using Gameplay.Feature.FinishLevel.System;
using Gameplay.Feature.MapGenerator.System;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature.FinishLevel
{
	public sealed class FinishLevelFeature : Infrastructure.ECS.Feature
	{
		public FinishLevelFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddInit<CreateFinishLevelObserverSystem>();
			AddInit<CreateFinishLevelDoorSystem>();
			
			AddUpdate<EnterToFinishLevelDoorProcessSystem>();
			AddUpdate<ExitFormFinishLevelDoorProcessSystem>();
			
			AddUpdate<HeroHealthObserveSystem>();
			AddUpdate<GameTimerObserverSystem>();
			AddUpdate<EnemyQuantityObserverSystem>();
			
			AddUpdate<FinishLevelSystem>();
		}
	}
}