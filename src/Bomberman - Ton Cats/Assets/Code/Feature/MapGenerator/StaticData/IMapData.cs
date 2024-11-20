using StaticData;
using UnityEngine;

namespace Feature.MapGenerator.StaticData
{
	public interface IMapData : IStaticData
	{
		Vector2Int MapSize { get; }
		float EnemyFrequency { get; }
		int EnemySpawnDistanceToHero { get; }
	}
}