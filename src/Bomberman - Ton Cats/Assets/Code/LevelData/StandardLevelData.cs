using Gameplay.Map;
using Infrastructure.Boot;
using Infrastructure.ECS;

namespace LevelData
{
	public sealed class StandardLevelData : ILevelData
	{
		public IMap Map { get; set; }
		public IEcsRunner EcsRunner { get; set; }
		public IDevSceneRunner DevSceneRunner { get; set; }
	}
}