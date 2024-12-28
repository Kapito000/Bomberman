using Gameplay.Navigation.System;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Navigation
{
	public sealed class NavigationFeature : Infrastructure.ECS.Feature
	{
		public NavigationFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddUpdate<UpdateAgentDestinationSystem>();
		}
	}
}