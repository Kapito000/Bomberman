using Gameplay.Feature.MapGenerator.System;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature.MapGenerator
{
	public sealed class MapGenerationFeature : Infrastructure.ECS.Feature
	{
		public MapGenerationFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddInit<CreateMapSystem>();
			AddInit<CreateGroundSystem>();
			AddInit<CreateIndestructibleTilesSystem>();
			AddInit<CreateHeroSpawnPointSystem>();
			AddInit<CreateEnemySpawnPointSystem>();
			AddInit<CreateDestructibleTilesSystem>();
			AddInit<FinishMapGeneratrionSystem>();
			AddInit<BindNavMeshSystem>();
		}
	}
}