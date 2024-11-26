using Gameplay.Feature.MapGenerator.StaticData;

namespace Gameplay.Feature.MapGenerator.Services.DestructibleTilesPlacer
{
	public sealed class StandardDestructibleTilesPlacer : IDestructibleTilesPlacer
	{
		readonly IMapData _mapData;

		public StandardDestructibleTilesPlacer(IMapData mapData)
		{
			_mapData = mapData;
		}

		public float PlaceFrequency() =>
			_mapData.DestructibleFrequency;
	}
}