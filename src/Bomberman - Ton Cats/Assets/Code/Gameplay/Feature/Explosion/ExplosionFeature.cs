using Gameplay.Feature.Explosion.System;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature.Explosion
{
	public sealed class ExplosionFeature : Infrastructure.ECS.Feature
	{
		public ExplosionFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddUpdate<StartExplosionSystem>();
			AddUpdate<ScanExplosionAreaSystem>();
			AddUpdate<CreateBlowUpDestructibleSystem>();
			AddUpdate<CreateExplosionCenterSystem>();
			AddUpdate<ExplosionCenterAudioEffectSystem>();
			AddUpdate<CreateExplosionPartSystem>();
			AddUpdate<AddToTargetBufferSystem>();
			AddUpdate<RemoveFromTargetBufferSystem>();
			AddUpdate<ExplosionDamageSystem>();
			
			AddCleanup<CleanupSystem>();
		}
	}
}