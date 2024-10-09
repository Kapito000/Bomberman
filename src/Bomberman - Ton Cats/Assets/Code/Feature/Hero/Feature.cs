using Feature.Hero.System;
using Infrastructure.SystemFactory;
using Leopotam.EcsLite;

namespace Feature.Hero
{
	public class Feature : Infrastructure.Feature
	{
		public Feature(EcsSystems systems, ISystemFactory systemFactory)
			: base(systems, systemFactory)
		{
			Add<HeroSystem>();
		}
	}
}