using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature.Bonus
{
	public sealed class BonusFeature : Infrastructure.ECS.Feature 
	{
		public BonusFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			
		}
	}
}