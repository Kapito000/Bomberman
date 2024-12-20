using Gameplay.Feature.Enemy.Base.Factory;
using Leopotam.EcsLite;
using Zenject;

namespace Gameplay.Feature.Enemy.System
{
	public sealed class CreateEnemyCounterSystem : IEcsRunSystem
	{
		[Inject] IEnemyFactory _enemyFactory;
		
		public void Run(IEcsSystems systems)
		{
			_enemyFactory.CreateEnemyCounter();
		}
	}
}