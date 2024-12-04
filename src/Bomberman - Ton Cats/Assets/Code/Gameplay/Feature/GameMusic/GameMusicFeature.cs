using Gameplay.Feature.GameMusic.System;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature.GameMusic
{
	public sealed class GameMusicFeature : Infrastructure.ECS.Feature
	{
		public GameMusicFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddInit<CreateMusicSystem>();
		}
	}
}