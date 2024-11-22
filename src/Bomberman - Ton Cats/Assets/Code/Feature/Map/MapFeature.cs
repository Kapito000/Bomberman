using Factory.SystemFactory;
using Feature.Map.System;

namespace Feature.Map
{
	public sealed class MapFeature : Infrastructure.ECS.Feature
	{
		public MapFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddUpdate<DestroyTileSystem>();
			AddUpdate<RebakeNavigationSurfaceSystem>();
			
			AddCleanup<CleanupSystem>();
		}
	}
}