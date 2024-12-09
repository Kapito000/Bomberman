using Gameplay.Feature.Audio.Behaviour;

namespace Gameplay.Feature.Audio.Component
{
	public struct DespawnRequest { }
	public struct EndPlayDespawn { }
	public struct PlayClipAtPointRequest { }
	public struct PooledAudioSourcePool { public PooledAudioSource.Pool Value; }
	public struct PooledAudioSourceComponent { public PooledAudioSource Value; }
}