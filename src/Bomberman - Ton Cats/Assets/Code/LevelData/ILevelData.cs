using Infrastructure.Boot;
using Infrastructure.ECS;

namespace LevelData
{
	public interface ILevelData
	{
		IEcsRunner EcsRunner { get; set; }
		IDevSceneRunner DevSceneRunner { get; set; }
	}
}