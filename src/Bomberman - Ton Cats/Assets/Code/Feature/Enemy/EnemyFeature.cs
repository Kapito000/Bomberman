using Factory.SystemFactory;
using Feature.Enemy.Base.System;
using Feature.Enemy.System;

namespace Feature.Enemy
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