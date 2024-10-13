using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Feature.Destruction.System
{
	public sealed class DestructionSystem : EcsSystem, IEcsRunSystem
	{
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