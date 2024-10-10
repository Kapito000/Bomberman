using Extensions;
using Factory.HeroFactory;
using Feature.Hero.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.Mathematics;
using UnityEngine;
using Zenject;
using Transform = Common.Component.Transform;

namespace Feature.Hero.System
{
	public sealed class SpawnHeroSystem : EcsSystem, IEcsInitSystem
	{
		[Inject] IHeroFactory _heroFactory;

		readonly EcsFilterInject<Inc<HeroSpawnPoint, Transform>> _filter;

		public void Init(IEcsSystems systems)
		{
			foreach (var spawnPointEntity in _filter.Value)
			{
				var transform = _world.Transform(spawnPointEntity);
				Vector2 pos = transform.position;
				var heroObj = _heroFactory
					.CreateHero(pos, quaternion.identity, transform);
				
				if (heroObj.TryAddToEcs(_world, out var heroEntity) == false)
					continue;
				
				_world.AddComponent<Component.Hero>(heroEntity);
			}
		}
	}
}