using Gameplay.Feature.Life.Component;
using Gameplay.Feature.Life.System;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature.Life
{
	public sealed class LifeFeature : Infrastructure.ECS.Feature
	{
		public LifeFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddUpdate<ChangeLifePointsSystem>();
			AddUpdate<DeathSystem>();
			AddUpdate<DeathAudioEffectSystem>();
			AddUpdate<ImmortalTimerSystem>();

			AddCleanup<CleanupSystem>();
		}
	}
}