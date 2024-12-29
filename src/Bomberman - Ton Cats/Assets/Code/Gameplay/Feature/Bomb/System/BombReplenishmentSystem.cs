using Gameplay.Feature.Bomb.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Bomb.System
{
	public sealed class BombReplenishmentSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _bombCarrier;

		readonly EcsFilterInject<Inc<BombCarrier, BombStack>> _bombCarrierFilter;
		readonly EcsFilterInject<Inc<BombComponent>> _bombFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var carrierEntity in _bombCarrierFilter.Value)
			{
				_bombCarrier.SetEntity(carrierEntity);
				var bombStack = _bombCarrier.BombStack();
				if (bombStack.Count != 0)
					continue;

				var bombCount = _bombFilter.Value.GetEntitiesCount();
				if (bombCount == 0)
					bombStack.Push(BombType.Usual);
			}
		}
	}
}