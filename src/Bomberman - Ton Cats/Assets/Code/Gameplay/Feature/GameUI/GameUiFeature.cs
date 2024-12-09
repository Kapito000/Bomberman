using Gameplay.UI.System;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature.GameUI
{
	public sealed class GameUiFeature : Infrastructure.ECS.Feature
	{
		public GameUiFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddInit<CreateRootCanvasSystem>();
			AddInit<CreateWindowsRootSystem>();
			AddInit<CreateWindowsSystem>();
		}
	}
}