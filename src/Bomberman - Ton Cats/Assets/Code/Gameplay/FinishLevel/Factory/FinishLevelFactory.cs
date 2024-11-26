using Gameplay.FinishLevel.Component;
using Infrastructure.ECS;
using Zenject;

namespace Gameplay.FinishLevel.Factory
{
	public sealed class FinishLevelFactory : IFinishLevelFactory
	{
		[Inject] EntityWrapper _entity;

		public int CreateFinishLevelObserver()
		{
			return _entity.NewEntity()
				.Add<FinishLevelObserver>()
				.Value;
		}
	}
}