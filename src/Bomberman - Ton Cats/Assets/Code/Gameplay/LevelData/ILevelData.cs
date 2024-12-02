using Gameplay.Feature.Map.MapController;
using Infrastructure;
using Infrastructure.Boot;
using Infrastructure.ECS;
using Leopotam.EcsLite;

namespace Gameplay.LevelData
{
	public interface ILevelData : IService
	{
		EcsWorld World { get; set; }
		IEcsRunner EcsRunner { get; set; }
		IMapController MapController { get; set; }
		IDevSceneRunner DevSceneRunner { get; set; }
	}
}