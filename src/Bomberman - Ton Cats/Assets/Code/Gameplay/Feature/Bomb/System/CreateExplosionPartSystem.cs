using Common.Component;
using Gameplay.Feature.Bomb.Component;
using Gameplay.Feature.Bomb.Factory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Bomb.System
{
	public sealed class CreateExplosionPartSystem : IEcsRunSystem
	{
		readonly EcsFilterInject<
			Inc<CreateExplosionRequest, Position, Direction, ExplosionPartComponent,
				ForParent>> _requestFilter;

		[Inject] IBombFactory _factory;
		[Inject] EntityWrapper _request;

		public void Run(IEcsSystems systems)
		{
			foreach (var request in _requestFilter.Value)
			{
				_request.SetEntity(request);

				var pos = _request.Position();
				var dir = _request.Direction();
				var parent = _request.ForParent();
				var part = _request.ExplosionPart();
				_factory.CreateExplosionPart(pos, dir, parent, part);
			}
		}
	}
}