using Gameplay.Feature.Bonus.Factory;
using Leopotam.EcsLite;
using Zenject;

namespace Gameplay.Feature.Bonus.System
{
	public sealed class CreateBonusParentSystem : IEcsRunSystem
	{
		[Inject] IBonusFactory _bonusFactory;
		
		public void Run(IEcsSystems systems)
		{
			_bonusFactory.CreateBonusParent();
		}
	}
}