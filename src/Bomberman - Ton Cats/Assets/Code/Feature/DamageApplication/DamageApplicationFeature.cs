using Factory.SystemFactory;
using Feature.DamageApplication.System;

namespace Feature.DamageApplication
{
	public sealed class DamageApplicationFeature : Infrastructure.ECS.Feature
	{
		public DamageApplicationFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddUpdate<ApplyDamageSystem>();
			AddCleanup<CleanupSystem>();
		}
	}
}