using Gameplay.Feature.Bomb.Factory;
using Leopotam.EcsLite;
using Zenject;

namespace Gameplay.Feature.Bomb.System
{
	public sealed class CreateBombParentSystem : IEcsRunSystem
	{
		[Inject] IBombFactory _bombFactory;
		
		public void Run(IEcsSystems systems)
		{
			_bombFactory.CreateBombParent();
		}
	}
}