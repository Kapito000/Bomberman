using Gameplay.Feature.Collisions.Component;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Collisions.System
{
	public sealed class ClenupSystem : IEcsRunSystem
	{
		[Inject] EcsWorld _world;

		readonly EcsFilterInject<Inc<TriggerExitBuffer>> _triggerExitBufferFilter;
		readonly EcsFilterInject<Inc<TriggerEnterBuffer>> _triggerEnterBufferFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _triggerEnterBufferFilter.Value)
				_world.GetPool<TriggerEnterBuffer>().Del(e);
			foreach (var e in _triggerExitBufferFilter.Value)
				_world.GetPool<TriggerExitBuffer>().Del(e);
		}
	}
}