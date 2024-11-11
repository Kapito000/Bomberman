using Common.Component;
using Feature.Enemy.Base.Component;
using Feature.Enemy.Base.Factory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Feature.Enemy.Base.System
{
	public sealed class CreateBaseEnemy : IEcsInitSystem
	{
		[Inject] EntityWrapper _spawnPoint;
		[Inject] IBaseEnemyFactory _baseEnemyFactory;

		readonly EcsFilterInject<
			Inc<EnemySpawnPoint, TransformComponent>> _spawnPointFilter;

		public void Init(IEcsSystems systems)
		{
			foreach (var spawnPoint in _spawnPointFilter.Value)
			{
				_spawnPoint.SetEntity(spawnPoint);
				var pos = _spawnPoint.TransformPos();
				var tr = _spawnPoint.Transform();
				_baseEnemyFactory.CreateEnemy(pos, tr);
			}
		}
	}
}