using Gameplay.Feature.GameMusic.System;
using Gameplay.Feature.MainMenu.System;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature.MainMenu
{
	public sealed class MainMenuFeature : Infrastructure.ECS.Feature
	{
		public MainMenuFeature(ISystemFactory systemFactory)
			: base(systemFactory)
		{
			AddInit<CreateMusicParentSystem>();
			AddInit<CreateMainMenuMusiSystem>();
		}
	}
}