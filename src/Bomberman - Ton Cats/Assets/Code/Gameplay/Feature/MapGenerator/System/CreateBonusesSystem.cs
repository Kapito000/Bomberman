using Gameplay.Feature.Bonus.Component;
using Gameplay.Feature.MapGenerator.Services;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.MapGenerator.System
{
	public sealed class CreateBonusesSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _bonus;
		[Inject] EntityWrapper _bonusCollection;
		[Inject] IMapGenerator _mapGenerator;

		readonly EcsFilterInject<Inc<BonusComponent, BonusType>> _bonusesFilter;

		public void Run(IEcsSystems systems)
		{
			_mapGenerator.CreateBonuses();
		}
	}
}