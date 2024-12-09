using Gameplay.Feature.Audio.Component;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Audio.System
{
	public sealed class CleanupSystem : IEcsRunSystem
	{
		[Inject] EcsWorld _world;

		readonly EcsFilterInject<Inc<PlayClipAtPointRequest>>
			_playClipAtPointRequestFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _playClipAtPointRequestFilter.Value)
				_world.DelEntity(e);
		}
	}
}