using Gameplay.LevelData;

namespace Infrastructure.GameStatus.State
{
	public static class Util
	{
		public static bool TryDevStart(ILevelData levelData)
		{
			var devSceneRunner = levelData.DevSceneRunner;
			if (devSceneRunner == null)
				return false;
			return devSceneRunner.TryRunScene();
		}
	}
}