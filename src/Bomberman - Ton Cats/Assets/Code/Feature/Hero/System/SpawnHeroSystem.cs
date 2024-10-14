using Extensions;
using Feature.Hero.Component;
using Feature.Hero.Factory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using StaticData.Hero;
using Unity.Mathematics;
using UnityEngine;
using Zenject;
using Transform = Common.Transform;

namespace Feature.Hero.System
{
	public sealed class SpawnHeroSystem : EcsSystem, IEcsInitSystem
	{
		[Inject] IHeroData _heroData;
		[Inject] IHeroFactory _heroFactory;

		readonly EcsFilterInject<Inc<HeroSpawnPoint, Transform>> _filter;

		public void Init(IEcsSystems systems)
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