using Feature.Bomb.Factory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Zenject;

namespace Feature.Bomb.System
{
	public sealed class CreateBombParentSystem : EcsSystem, IEcsInitSystem
	{
		[Inject] IBombFactory _bombFactory;
		
		public void Init(IEcsSystems systems)
		{
			_bombFactory.CreateBombParent();
		}
	}
}