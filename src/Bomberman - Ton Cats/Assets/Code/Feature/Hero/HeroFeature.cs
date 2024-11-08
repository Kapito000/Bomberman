﻿using Factory.SystemFactory;
using Feature.Hero.System;

namespace Feature.Hero
{
	public sealed class HeroFeature : Infrastructure.ECS.Feature
	{
		public HeroFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddInit<SpawnHeroSystem>();
			AddUpdate<HeroMovementSystem>();
			AddUpdate<HeroAnimationSystem>();
		}
	}
}