using Gameplay.Feature.Enemy.Base.System;
using Gameplay.Feature.Enemy.System;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature.Enemy
{
	public sealed class EnemyFeature : Infrastructure.ECS.Feature
	{
		public EnemyFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddInit<CreateBaseEnemySystem>();
			
			AddUpdate<EnemyAIBrainProcessSystem>();
			AddUpdate<AttackOfHeroSystemSystem>();
		}
	}
}