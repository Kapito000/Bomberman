using Feature.Destruction.Component;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Feature.Destruction.System
{
	public sealed class DestructionSystem : IEcsRunSystem
	{
		[Inject] EcsWorld _world;
		
		readonly EcsFilterInject<Inc<Destructed>> _filter;
		
		public void Run(IEcsSystems systems)
		{
			foreach (var e in _filter.Value)
			{
				_world.DelEntity(e);
			}
		}
	}
}