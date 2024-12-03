using Gameplay.Feature.Music.System;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature.Music
{
	public sealed class MusicFeature : Infrastructure.ECS.Feature
	{
		public MusicFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddInit<CreateMusicSystem>();
		}
	}
}