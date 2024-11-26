namespace Infrastructure.FinishLevel.Condition
{
	public sealed class HeroHealthCondition : IHeroHealthCondition
	{
		public bool Value { get; private set; }

		public void SetValue(bool value) =>
			Value = value;
	}
}