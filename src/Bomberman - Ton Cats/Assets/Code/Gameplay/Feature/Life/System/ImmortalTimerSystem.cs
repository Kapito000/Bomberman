using Gameplay.Feature.Life.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.Life.System
{
	public sealed class ImmortalTimerSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _immortal;

		readonly EcsFilterInject<Inc<Immortal, ImmortalTimer>> _immortalFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _immortalFilter.Value)
			{
				_immortal.SetEntity(e);
				var timerEnd = _immortal.ImmortalTimer();
				if (Time.time > timerEnd)
				{
					_immortal
						.Remove<Immortal>()
						.Remove<ImmortalTimer>();
				}
			}
		}
	}
}