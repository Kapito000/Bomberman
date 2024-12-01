using Gameplay.Feature.MapGenerator.StaticData;

namespace Gameplay.Feature.MapGenerator.Services.SubGenerator
{
	public sealed class StandardDestructibleTilesPlacer
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