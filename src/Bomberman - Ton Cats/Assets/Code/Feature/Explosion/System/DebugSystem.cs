using Extensions;
using Feature.Explosion.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;

namespace Feature.Explosion.System
{
	// Delete me.👇
	public sealed class DebugSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _entity;

		readonly EcsFilterInject<
			Inc<Component.Explosion, TargetsBuffer>> _explosionFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _explosionFilter.Value)
			{
				_entity.SetEntity(e);
				foreach (var pack in _entity.TargetsBuffer())
				{
					if (pack.Unpack(out var target))
					{
						Debug.Log(target);
					}
				}
			}
		}
	}
}