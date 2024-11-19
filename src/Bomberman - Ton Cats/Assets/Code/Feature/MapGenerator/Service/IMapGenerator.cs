using Cysharp.Threading.Tasks;
using Gameplay.Map;
using Infrastructure;

namespace Feature.MapGenerator.Service
{
	public interface IMapGenerator : IService
	{
		UniTask<IMap> CreateMapAsync(IGenerateMapProgress progressReporter);
	}
}