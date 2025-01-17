using Gameplay.Feature.BonusApplication.System;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature.BonusApplication
{
	public class BonusApplicationFeature : Infrastructure.ECS.Feature
	{
		public BonusApplicationFeature(ISystemFactory systemFactory) : base(
			systemFactory)
		{
			AddUpdate<AddLifeBonusApplicationSystem>();
		}
	}
}