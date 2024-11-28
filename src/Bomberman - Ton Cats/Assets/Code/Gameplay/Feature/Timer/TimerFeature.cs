using Gameplay.Feature.Timer.System;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature.Timer
{
	public sealed class TimerFeature : Infrastructure.ECS.Feature
	{
		public TimerFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddInit<CreateTimerSystem>();
			
			AddUpdate<GameTimerCounterSystem>();
		}
	}
}