using UnityEngine;
using Menu = Constant.CreateAssetMenu;

namespace Gameplay.Feature.MapGenerator.StaticData
{
	[CreateAssetMenu(menuName = Menu.Path.c_StaticData + nameof(MapData))]
	public sealed class MapData : ScriptableObject, IMapData
	{
		[field: SerializeField] public Vector2Int MapSize { get; private set; }
		[field: SerializeField] public float EnemyFrequency { get; private set; } = .1f;
		[field: SerializeField] public float DestructibleFrequency { get; private set; } = .5f;
		[field: SerializeField] public int EnemySpawnDistanceToHero { get; private set; } = 5;
	}
}