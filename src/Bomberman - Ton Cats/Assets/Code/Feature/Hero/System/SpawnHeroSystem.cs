using Extensions;
using Factory.HeroFactory;
using Feature.Hero.Component;
using Feature.Input.Component;
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
		[Inject] EntityWrapper _heroEntity;

		readonly EcsFilterInject<Inc<HeroSpawnPoint, Transform>> _filter;

		public void Init(IEcsSystems systems)
		{
			foreach (var spawnPointEntity in _filter.Value)
			{
				var transform = _world.Transform(spawnPointEntity);
				Vector2 pos = transform.position;
				var heroEntity = _heroFactory
					.CreateHero(pos, quaternion.identity, transform);
				_heroEntity.SetEntity(heroEntity);

				_heroEntity
					.Add<Component.Hero>()
					.Add<InputReader>()
					;
			}
		}
	}
}