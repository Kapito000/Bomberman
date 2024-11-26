using Common.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Destruction.System
{
	public sealed class CommonCleanupSystem : IEcsRunSystem
	{
		readonly EcsFilterInject<Inc<FirstBreath>> _firstBreathFilter;

		[Inject] EntityWrapper _entity;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _firstBreathFilter.Value)
			{
				_entity.SetEntity(e);
				_entity.Remove<FirstBreath>();
			}
		}
	}
}