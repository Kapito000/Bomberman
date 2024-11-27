namespace Infrastructure.FinishLevel
{
	public interface IFinishLevelService : IService
	{
		bool GameOver(int observerEntity);
		bool LevelComplete(int observerEntity);
		void SwitchGameToMainMenu();
	}
}