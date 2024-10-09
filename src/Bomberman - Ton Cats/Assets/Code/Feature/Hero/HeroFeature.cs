using Feature.Hero.System;
using Infrastructure.SystemFactory;

namespace Feature.Hero
{
	public class HeroFeature : Infrastructure.Feature
	{
		public HeroFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddUpdate<HeroSystem>();
		}
	}
}