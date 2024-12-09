using Gameplay.Feature.Audio.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Audio.System
{
	public sealed class PooledAudioSourceDespawnSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _pooledAudioSource;

		readonly EcsFilterInject<
				Inc<PooledAudioSourceComponent, PooledAudioSourcePool, DespawnRequest>>
			_pooledItemFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _pooledItemFilter.Value)
			{
				_pooledAudioSource.SetEntity(e);

				var pool = _pooledAudioSource.PooledAudioSourcePool();
				var pooledItem = _pooledAudioSource.PooledAudioSource();
				pool.Despawn(pooledItem);
				_pooledAudioSource.Remove<DespawnRequest>();
			}
		}
	}
}