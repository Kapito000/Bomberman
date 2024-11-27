using Gameplay.Feature.FinishLevel.System;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature.FinishLevel
{
	public sealed class FinishLevelFeature : Infrastructure.ECS.Feature
	{
		public FinishLevelFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddInit<CreateFinishLevelObserverSystem>();
			
			AddUpdate<HeroHealthObserveSystem>();
			AddUpdate<FinishLevelSystem>();
		}
	}
}