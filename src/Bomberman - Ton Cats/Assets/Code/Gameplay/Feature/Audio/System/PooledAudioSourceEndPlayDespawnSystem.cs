using Common.Component;
using Gameplay.Feature.Audio.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Audio.System
{
	public sealed class PooledAudioSourceEndPlayDespawnSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _pooledAudioSource;

		readonly EcsFilterInject<
				Inc<PooledAudioSourceComponent, EndPlayDespawn, AudioSourceComponent>,
				Exc<DespawnRequest>>
			_pooledAudioSourceFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _pooledAudioSourceFilter.Value)
			{
				_pooledAudioSource.SetEntity(e);
				var audioSource = _pooledAudioSource.AudioSource();
				if (audioSource.isPlaying == false)
				{
					_pooledAudioSource.Add<DespawnRequest>();
					_pooledAudioSource.Remove<EndPlayDespawn>();
				}
			}
		}
	}
}