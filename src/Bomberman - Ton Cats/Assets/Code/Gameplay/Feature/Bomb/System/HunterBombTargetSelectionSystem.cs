using Common.Component;
using Gameplay.Feature.Bomb.Component;
using Gameplay.Feature.Enemy.Base.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;
using Vector2 = Extensions.Vector2;

namespace Gameplay.Feature.Bomb.System
{
	public sealed class HunterBombTargetSelectionSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _bomb;
		[Inject] EntityWrapper _enemy;

		readonly EcsFilterInject<Inc<BombComponent, BombHunter,
				NavMeshAgentComponent, TransformComponent>>
			_bombFilter;
		readonly EcsFilterInject<Inc<EnemyComponent, TransformComponent>>
			_enemyFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var bombEntity in _bombFilter.Value)
			{
				_bomb.SetEntity(bombEntity);
				var target = FindTarget(_bomb);
				if (target != null)
				{
					_bomb.ReplaceBombHunterTarget(target);
					_bomb.ReplaceAgentDestination(target.position);
				}
			}
		}

		Transform FindTarget(EntityWrapper bomb)
		{
			Transform target = null;
			var bombPos = bomb.TransformPos();
			var mainSqrDistance = float.PositiveInfinity;

			foreach (var enemyComponent in _enemyFilter.Value)
			{
				_enemy.SetEntity(enemyComponent);
				var enemyTr = _enemy.Transform();
				var enemyPos = enemyTr.position;

				if (target == null)
				{
					target = enemyTr;
					mainSqrDistance = Vector2.SqrDistance(bombPos, enemyTr.position);
					continue;
				}

				var sqrDistance = Vector2.SqrDistance(bombPos, enemyPos);

				if (sqrDistance < mainSqrDistance)
				{
					target = enemyTr;
					mainSqrDistance = sqrDistance;
				}
			}
			return target;
		}
	}
}