namespace Infrastructure.FinishLevel
{
	public sealed class FinishLevelService : IFinishLevelService
	{
		ICondition[] _conditions;

		public bool CanFinishLevel { get; } = false;

		public FinishLevelService()
		{
			_conditions = new ICondition[] { };
		}
	}
}