using Common.Component;
using Feature.Explosion.Component;
using Feature.Explosion.Factory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Feature.Explosion.System
{
	public sealed class CreateExplosionCenterSystem : IEcsRunSystem
	{
		readonly EcsFilterInject<
				Inc<CreateExplosionRequest, ExplosionCenter, Position, ForParent>>
			_requestFilter;

		[Inject] EntityWrapper _request;
		[Inject] IExplosionFactory _explosionFactory;

		public void Run(IEcsSystems systems)
		{
			foreach (var request in _requestFilter.Value)
			{
				_request.SetEntity(request);

				var pos = _request.Position();
				var parent = _request.ForParent();
				_explosionFactory.CreateExplosionCenter(pos, parent);
			}
		}
	}
}