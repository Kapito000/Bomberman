using Factory.SystemFactory;
using Feature.Explosion.System;

namespace Feature.Explosion
{
	public sealed class ExplosionFeature : Infrastructure.ECS.Feature
	{
		public ExplosionFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddUpdate<StartExplosionSystem>();
			AddUpdate<ScanExplosionAreaSystem>();
			AddUpdate<CreateBlowUpDestructibleSystem>();
			AddUpdate<CreateExplosionCenterSystem>();
			AddUpdate<CreateExplosionPartSystem>();
			AddUpdate<AddToTargetBufferSystem>();
			AddUpdate<RemoveFromTargetBufferSystem>();
			AddUpdate<DebugSystem>();
			
			AddCleanup<CreateExplosionRequestCleanupSystem>();
		}
	}
}