using Gameplay.EndGame.System;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.EndGame
{
	public sealed class EndGameFeature : Infrastructure.ECS.Feature
	{
		public EndGameFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddInit<CreateFinishLevelObserverSystem>();
			
			AddUpdate<HeroHealthObserveSystem>();
			AddUpdate<EndGameSystem>();
		}
	}
}