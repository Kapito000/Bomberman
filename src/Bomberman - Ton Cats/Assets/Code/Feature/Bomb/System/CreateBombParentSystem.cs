using Feature.Bomb.Factory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Zenject;

namespace Feature.Bomb.System
{
	public sealed class CreateBombParentSystem : EcsSystem, IEcsRunSystem
	{
		[Inject] IBombFactory _bombFactory;
		
		public void Run(IEcsSystems systems)
		{
			_bombFactory.CreateBombParent();
		}
	}
}