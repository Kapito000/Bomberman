using Gameplay.Map;
using Infrastructure;

namespace Feature.MapGenerator.Services
{
	public interface IMapGenerator : IService
	{
		IMap CreateMap();
	}
}