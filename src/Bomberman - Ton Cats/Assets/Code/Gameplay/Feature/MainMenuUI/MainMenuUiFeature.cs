using Gameplay.UI.System;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature.MainMenuUI
{
	public sealed class MainMenuUiFeature : Infrastructure.ECS.Feature
	{
		public MainMenuUiFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddInit<CreateRootCanvasSystem>();
			AddInit<CreateWindowsRootSystem>();
			AddInit<CreateWindowsSystem>();
		}
	}
}