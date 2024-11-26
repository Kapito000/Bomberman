using Gameplay.Feature.MapGenerator.StaticData;

namespace Gameplay.Feature.MapGenerator.Services.EnemyPlacer
{
	public sealed class StandardEnemyPlacer : IEnemyPlacer
	{
		readonly IMapData _mapData;

		public StandardEnemyPlacer(IMapData mapData)
		{
			_mapData = mapData;
		}

		public float PlaceFrequency() =>
			_mapData.EnemyFrequency;
	}
}