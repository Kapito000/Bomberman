namespace StaticData.SceneNames
{
	public interface ISceneNameData : IStaticData
	{
		string BootSceneName { get; }
		string FirstGameSceneName { get; }
	}
}