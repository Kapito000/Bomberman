using Common.Component;
using Feature.Explosion.Component;
using Feature.Explosion.Factory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Feature.Explosion.System
{
	public sealed class CreateExplosionPartSystem : IEcsRunSystem
	{
		readonly EcsFilterInject<
			Inc<CreateExplosionRequest, Position, Direction, ExplosionPart,
				ForParent>> _requestFilter;

		[Inject] EntityWrapper _request;
		[Inject] IExplosionFactory _explosionFactory;

		public void Run(IEcsSystems systems)
		{
			foreach (var request in _requestFilter.Value)
			{
				_request.SetEntity(request);

				var pos = _request.Position();
				var dir = _request.Direction();
				var parent = _request.ForParent();
				var part = _request.ExplosionPart();
				_explosionFactory.CreateExplosionPart(pos, dir, parent, part);
			}
		}
	}
}