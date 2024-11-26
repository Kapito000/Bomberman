using Gameplay.Map;
using Infrastructure;

namespace Gameplay.Feature.MapGenerator.Services
{
	public interface IMapGenerator : IService
	{
		IMap CreateMap();
		IMap Map { get; }
	}
}