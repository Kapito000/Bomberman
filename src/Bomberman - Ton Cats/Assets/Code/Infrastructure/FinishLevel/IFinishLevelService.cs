namespace Infrastructure.FinishLevel
{
	public interface IFinishLevelService : IService
	{
		bool CanFinishLevel { get; }
	}
}