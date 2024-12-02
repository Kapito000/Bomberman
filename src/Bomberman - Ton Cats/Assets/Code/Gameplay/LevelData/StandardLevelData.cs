using Gameplay.Feature.Map.MapController;
using Infrastructure.Boot;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Mitfart.LeoECSLite.UnityIntegration;

namespace Gameplay.LevelData
{
	public sealed class StandardLevelData : ILevelData
	{
		public EcsWorld World { get; set; }
		public IEcsRunner EcsRunner { get; set; }
		public IMapController MapController { get; set; }
		public IDevSceneRunner DevSceneRunner { get; set; }
#if UNITY_EDITOR
		public EcsWorldDebugSystem EcsWorldDebugSystem { get; set; }
#endif
	}
}