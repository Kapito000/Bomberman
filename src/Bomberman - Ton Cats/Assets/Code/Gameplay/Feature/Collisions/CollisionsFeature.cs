using Gameplay.Feature.Collisions.System;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature.Collisions
{
	public sealed class CollisionsFeature : Infrastructure.ECS.Feature
	{
		public CollisionsFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddCleanup<ClenupSystem>();
		}
	}
}