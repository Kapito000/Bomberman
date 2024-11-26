﻿using Infrastructure;
using Infrastructure.Boot;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Map;

namespace LevelData
{
	public interface ILevelData : IService
	{
		EcsWorld World { get; set; }
		IEcsRunner EcsRunner { get; set; }
		IDevSceneRunner DevSceneRunner { get; set; }
	}
}