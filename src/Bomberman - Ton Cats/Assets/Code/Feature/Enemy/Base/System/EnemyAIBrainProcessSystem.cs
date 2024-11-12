using Feature.Enemy.Base.Component;
using FluentBehaviourTree;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Feature.Enemy.Base.System
{
	public sealed class EnemyAIBrainProcessSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _enemy;
		
		readonly EcsFilterInject<Inc<EnemyAIStatus>> _enemyFilter;

		void Init()
		{
			var builder = new BehaviourTreeBuilder();
			var tree = builder
				// .
				.Build();
		}
		
		public void Run(IEcsSystems systems)
		{
			foreach (var enemy in _enemyFilter.Value)
			{
				_enemy.SetEntity(enemy);
			}
		}
	}
}