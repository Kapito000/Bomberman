using Common.Component;
using Extensions;
using Feature.Hero.Component;
using Feature.Hero.Factory;
using Feature.Hero.StaticData;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace Feature.Hero.System
{
	public sealed class SpawnHeroSystem : IEcsRunSystem
	{
		[Inject] EcsWorld _world;
		[Inject] IHeroData _heroData;
		[Inject] IHeroFactory _heroFactory;

		readonly EcsFilterInject<Inc<HeroSpawnPoint, TransformComponent>> _filter;

		public void Run(IEcsSystems systems)
		{
			foreach (var spawnPointEntity in _filter.Value)
			{
				var transform = _world.Transform(spawnPointEntity);
				Vector2 pos = transform.position;
				_heroFactory.CreateHero(pos, quaternion.identity, transform);
			}
		}
	}
}