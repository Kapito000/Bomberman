namespace Infrastructure.FinishLevel.Condition
{
	public interface ICondition
	{
		bool Check(int observerEntity);
	}
}