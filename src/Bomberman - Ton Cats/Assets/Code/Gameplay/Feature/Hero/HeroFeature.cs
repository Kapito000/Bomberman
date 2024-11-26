using Gameplay.Feature.Hero.System;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature.Hero
{
	public sealed class HeroFeature : Infrastructure.ECS.Feature
	{
		public HeroFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddInit<SpawnHeroSystem>();
			AddInit<InitHeroSkinSystem>();
			
			AddUpdate<HeroMovementSystem>();
			AddUpdate<HeroAnimationSystem>();
		}
	}
}