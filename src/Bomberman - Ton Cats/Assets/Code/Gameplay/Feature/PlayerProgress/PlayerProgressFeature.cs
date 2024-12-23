using Gameplay.Feature.PlayerProgress.System;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature.PlayerProgress
{
	public sealed class PlayerProgressFeature : Infrastructure.ECS.Feature
	{
		public PlayerProgressFeature(ISystemFactory systemFactory) : base(
			systemFactory)
		{
			AddInit<CreatePlayerProgrressSystem>();
			
			AddUpdate<IncreaseReachedLevelSystem>();
		}
	}
}