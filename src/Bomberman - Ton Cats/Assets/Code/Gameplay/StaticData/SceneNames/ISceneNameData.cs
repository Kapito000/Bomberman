namespace Gameplay.StaticData.SceneNames
{
	public interface ISceneNameData : IStaticData
	{
		string Boot { get; }
		string MainMenu { get; }
		string Game { get; }
	}
}