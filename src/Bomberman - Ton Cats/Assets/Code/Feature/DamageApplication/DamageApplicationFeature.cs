using Factory.SystemFactory;
using Feature.DamageApplication.System;

namespace Feature.DamageApplication
{
	public sealed class DamageApplicationFeature : Infrastructure.ECS.Feature
	{
		public DamageApplicationFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddUpdate<UpdateDamageBufferSystemSystem>();
			AddUpdate<DamageBufferToDamageSystem>();
			
			AddUpdate<ApplyDamageSystem>();
			
			AddUpdate<ApplyTakenDamageEffectSystem>();
			AddUpdate<DamageEffectProcessSystem>();
			AddUpdate<DamageEffectDurationTimerSystem>();
			
			AddCleanup<CleanupSystem>();
		}
	}
}