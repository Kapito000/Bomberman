using Feature.MapGenerator.StaticData;

namespace Feature.MapGenerator.Services.DestructibleTilesPlacer
{
	public sealed class StandardDestructibleTilesPlacer : IDestructibleTilesPlacer
	{
		readonly IMapData _mapData;

		public StandardDestructibleTilesPlacer(IMapData mapData)
		{
			_mapData = mapData;
		}

		public float PlaceFrequency() =>
			_mapData.EnemyFrequency;
	}
}