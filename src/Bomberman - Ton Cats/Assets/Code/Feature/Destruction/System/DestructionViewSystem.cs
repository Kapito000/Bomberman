using Common.Component;
using Feature.Destruction.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;

namespace Feature.Destruction.System
{
	public sealed class DestructionViewSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _destroyed;

		readonly EcsFilterInject<Inc<Destructed, View>> _filter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _filter.Value)
			{
				_destroyed.SetEntity(e);
				var view = _destroyed.View();
				view.Dispose();
				Object.Destroy(view.gameObject);
			}
		}
	}
}