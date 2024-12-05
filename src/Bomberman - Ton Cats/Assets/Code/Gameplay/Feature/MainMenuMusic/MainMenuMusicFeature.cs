using Gameplay.Feature.GameMusic.System;
using Gameplay.Feature.MainMenuMusic.System;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature.MainMenuMusic
{
	public sealed class MainMenuMusicFeature : Infrastructure.ECS.Feature
	{
		public MainMenuMusicFeature(ISystemFactory systemFactory)
			: base(systemFactory)
		{
			AddInit<CreateMusicParentSystem>();
			AddInit<CreateMainMenuMusiSystem>();
		}
	}
}