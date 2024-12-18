using Common.Component;
using Gameplay.Difficult;
using Gameplay.Feature.Enemy.Base.Component;
using Gameplay.Feature.FinishLevel.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Map.System
{
	public sealed class SpawnEnemyAtFinishLevelDoor : IEcsRunSystem
	{
		[Inject] EntityWrapper _door;
		[Inject] EntityWrapper _spawnRequest;
		[Inject] IDifficultService _difficultService;

		readonly EcsFilterInject<
			Inc<FinishLevelDoor, OpenEvent, TransformComponent>> _openedDoorFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var doorEntity in _openedDoorFilter.Value)
			{
				_door.SetEntity(doorEntity);
				var pos = _door.TransformPos();
				var enemyList = _difficultService.EnemyAtDoorForCurrentProgress();
				foreach (var pair in enemyList)
				{
					var enemyId = pair.Key;
					var count = pair.Value;
					for (int i = 0; i < count; i++)
					{
						_spawnRequest.NewEntity()
							.Add<EnemySpawnRequest>()
							.AddEnemyId(enemyId)
							.AddPosition(pos)
							;
					}
				}
			}
		}
	}
}