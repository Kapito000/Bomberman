using Gameplay.Feature.GameMusic.System;
using Gameplay.Feature.MainMenu.System;
using Gameplay.UI.System;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature.MainMenu
{
	public sealed class MainMenuFeature : Infrastructure.ECS.Feature
	{
		public MainMenuFeature(ISystemFactory systemFactory)
			: base(systemFactory)
		{
			AddInit<CreateRootCanvasSystem>();
			AddInit<CreateWindowsRootSystem>();
			AddInit<CreateWindowsSystem>();
			AddInit<CreateMainMenuUpperPanelSystem>();
			
			AddInit<InitManiMenuWindowSystem>();

			AddInit<CreateMusicParentSystem>();
			AddInit<CreateMainMenuMusiSystem>();
		}
	}
}