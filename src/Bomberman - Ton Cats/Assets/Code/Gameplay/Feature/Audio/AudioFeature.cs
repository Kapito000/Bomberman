using Gameplay.Feature.Audio.System;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature.Audio
{
	public sealed class AudioFeature : Infrastructure.ECS.Feature
	{
		public AudioFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddUpdate<PooledAudioSourceEndPlayDespawnSystem>();
			AddUpdate<PooledAudioSourceDespawnSystem>();
		}
	}
}