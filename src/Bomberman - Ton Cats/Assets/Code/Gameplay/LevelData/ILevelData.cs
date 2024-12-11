using Gameplay.Feature.Audio.Behaviour;
using Gameplay.Feature.Map.MapController;
using Infrastructure;
using Infrastructure.Boot;
using Infrastructure.ECS;
using Infrastructure.Factory.Kit;
using Leopotam.EcsLite;
using Mitfart.LeoECSLite.UnityIntegration;

namespace Gameplay.LevelData
{
	public interface ILevelData : IService
	{
		EcsWorld World { get; set; }
		IEcsRunner EcsRunner { get; set; }
		IDevSceneRunner DevSceneRunner { get; set; }
#if UNITY_EDITOR
		EcsWorldDebugSystem EcsWorldDebugSystem { get; set; }
#endif
	}

	public interface IGameLevelData : ILevelData
	{
		IMapController MapController { get; set; }
		PooledAudioSource.Pool AudioSourcePool { get; set; }
	}

	public interface IMainMenuLevelData : ILevelData
	{ }
}