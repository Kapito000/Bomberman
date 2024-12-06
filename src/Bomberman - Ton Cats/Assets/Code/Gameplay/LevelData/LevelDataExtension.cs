using Infrastructure.ECS;

namespace Gameplay.LevelData
{
	public static class LevelDataExtension
	{
		public static EntityWrapper NewEntityWrapper(this ILevelData levelData) => 
			new(levelData.World);
	}
}