using Gameplay.Map;
using Infrastructure;

namespace Gameplay.Feature.MapGenerator.Services
{
	public interface IMapGenerator : IService
	{
		void CreateMap();
		IGrid<TileType> TilesGrid { get; }
	}
}