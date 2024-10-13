using Common;
using Extensions;
using Feature.Bomb;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Feature.Input.System
{
	public sealed class PutBombRequestCleanupSystem : EcsSystem, IEcsRunSystem
	{
		readonly EcsFilterInject<Inc<PutBombRequest>> _filter;
		
		public void Run(IEcsSystems systems)
		{
			foreach (var e in _filter.Value)
			{
				_world.Remove<PutBombRequest>(e);
			}
		}
	}
}