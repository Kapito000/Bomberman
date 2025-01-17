using Gameplay.Feature.Bonus.System;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature.Bonus
{
	public sealed class BonusFeature : Infrastructure.ECS.Feature
	{
		public BonusFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddInit<CreateBonusParentSystem>();

			AddUpdate<SpawnBonusObjectSystem>();
			AddUpdate<SetBonusSpriteSystem>();
		}
	}
}