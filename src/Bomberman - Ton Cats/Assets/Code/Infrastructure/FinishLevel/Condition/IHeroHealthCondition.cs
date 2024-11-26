namespace Infrastructure.FinishLevel.Condition
{
	public interface IHeroHealthCondition : IGameOverCondition
	{
		void SetValue(bool value);
	}
}