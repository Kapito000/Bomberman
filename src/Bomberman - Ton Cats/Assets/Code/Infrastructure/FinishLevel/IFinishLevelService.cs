namespace Infrastructure.FinishLevel
{
	public interface IFinishLevelService : IService
	{
		bool GameOver { get; }
		bool LevelComplete { get; }
	}
}