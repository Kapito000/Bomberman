using Factory.SystemFactory;
using Feature.Life.System;

namespace Feature.Life
{
	public sealed class LifeFeature : Infrastructure.ECS.Feature
	{
		public LifeFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddUpdate<ChangeLifePointsSystem>();
			
			AddCleanup<CleanupSystem>();
		}
	}
}