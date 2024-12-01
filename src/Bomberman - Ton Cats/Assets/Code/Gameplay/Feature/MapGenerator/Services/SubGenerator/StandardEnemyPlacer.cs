using Gameplay.Feature.MapGenerator.StaticData;

namespace Gameplay.Feature.MapGenerator.Services.SubGenerator
{
	public sealed class StandardEnemyPlacer
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