using Factory.SystemFactory;
using Feature.Enemy.Base.System;

namespace Feature.Enemy.Base
{
	public sealed class EnemyFeature : Infrastructure.ECS.Feature
	{
		public EnemyFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddInit<CreateBaseEnemySystem>();
			AddUpdate<EnemyIaiBrainProcessSystem>();
		}
	}
}