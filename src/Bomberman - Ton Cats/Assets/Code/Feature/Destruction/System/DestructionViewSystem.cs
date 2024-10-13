using Common;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;

namespace Feature.Destruction.System
{
	public sealed class DestructionViewSystem : EcsSystem, IEcsRunSystem
	{
		[Inject] EntityWrapper _destroyed;

		readonly EcsFilterInject<Inc<Destructed, View>> _filter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _filter.Value)
			{
				_destroyed.SetEntity(e);
				Object.Destroy(_destroyed.View().gameObject);
			}
		}
	}
}