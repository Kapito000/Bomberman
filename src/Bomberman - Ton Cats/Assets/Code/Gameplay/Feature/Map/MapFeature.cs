using Gameplay.Feature.Map.System;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature.Map
{
	public sealed class MapFeature : Infrastructure.ECS.Feature
	{
		public MapFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddUpdate<ChangeMapProcessSystem>();
			AddUpdate<DestroyTileSystem>();
			AddUpdate<RebakeNavigationSurfaceSystem>();
			
			AddCleanup<CleanupSystem>();
		}
	}
}