using Gameplay.Feature.Bonus.System;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature.Bonus
{
	public sealed class SpawnBonusFeature : Infrastructure.ECS.Feature
	{
		public SpawnBonusFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddInit<CreateBonusParentSystem>();

			AddUpdate<SpawnBonusObjectSystem>();
			AddUpdate<SetBonusSpriteSystem>();
		}
	}
}