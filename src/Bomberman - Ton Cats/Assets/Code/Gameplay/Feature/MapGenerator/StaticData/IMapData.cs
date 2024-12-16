using Gameplay.StaticData;
using UnityEngine;

namespace Gameplay.Feature.MapGenerator.StaticData
{
	public interface IMapData : IStaticData
	{
		Vector2Int MapSize { get; }
		float DestructibleFrequency { get; }
		int EnemySpawnDistanceToHero { get; }
	}
}