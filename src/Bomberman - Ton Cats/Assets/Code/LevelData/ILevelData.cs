using Infrastructure;
using Infrastructure.Boot;
using Infrastructure.ECS;
using Map;

namespace LevelData
{
	public interface ILevelData : IService
	{
		IEcsRunner EcsRunner { get; set; }
		IDevSceneRunner DevSceneRunner { get; set; }
	}
}