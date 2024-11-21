﻿using Factory.SystemFactory;
using Feature.MapGenerator.System;

namespace Feature.MapGenerator
{
	public sealed class MapGenerationFeature : Infrastructure.ECS.Feature
	{
		public MapGenerationFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddInit<CreateMapSystem>();
			AddInit<CreateGroundSystem>();
			AddInit<CreateDestructibleTilesSystem>();
			AddInit<CreateIndestructibleTilesSystem>();
			AddInit<CreateHeroSpawnPointSystem>();
			AddInit<CreateEnemySpawnPointSystem>();
			AddInit<BindNavMeshSystem>();
		}
	}
}