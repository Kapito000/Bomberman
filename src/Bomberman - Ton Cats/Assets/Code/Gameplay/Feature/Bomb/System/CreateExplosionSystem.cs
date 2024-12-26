using Common.Component;
using Gameplay.Feature.Bomb.Component;
using Gameplay.Feature.Bomb.Factory;
using Gameplay.Feature.Map.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Bomb.System
{
	public sealed class CreateExplosionSystem : IEcsRunSystem
	{
		readonly EcsFilterInject<
				Inc<CreateExplosionRequest, ExplosionPartComponent, CellPos, Parent>>
			_requestFilter;

		[Inject] IBombFactory _factory;
		[Inject] EntityWrapper _request;

		public void Run(IEcsSystems systems)
		{
			foreach (var requestEntity in _requestFilter.Value)
			{
				_request.SetEntity(requestEntity);

				var dir = _request.Has<Direction>()
					? _request.Direction()
					: default;
				
				var cell = _request.CellPos();
				var parent = _request.Parent();
				var part = _request.ExplosionPart();
				_factory.CreateExplosionPart(cell, part, parent, dir);
				
				_request.Destroy();
			}
		}
	}
}