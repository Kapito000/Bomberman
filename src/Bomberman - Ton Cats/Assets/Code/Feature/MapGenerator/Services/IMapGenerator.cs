using Infrastructure;
using Map;

namespace Feature.MapGenerator.Services
{
	public interface IMapGenerator : IService
	{
		IMap CreateMap();
		IMap Map { get; }
	}
}