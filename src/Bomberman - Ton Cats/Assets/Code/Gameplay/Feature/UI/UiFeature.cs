using Gameplay.Feature.UI.System;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature.UI
{
	public sealed class UiFeature : Infrastructure.ECS.Feature
	{
		public UiFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddInit<CreateRootCanvasSystem>();
			AddInit<CreateWindowsRootSystem>();
		}
	}
}