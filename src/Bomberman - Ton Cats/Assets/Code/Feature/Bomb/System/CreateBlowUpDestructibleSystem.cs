using Common.Component;
using Feature.Bomb.Component;
using Feature.Bomb.Factory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;


namespace Feature.Bomb.System
{
	public sealed class CreateBlowUpDestructibleSystem : IEcsRunSystem
	{
		readonly EcsFilterInject<
				Inc<CreateExplosionRequest, BlowUpDestructible, Position, ForParent>>
			_requestFilter;

		[Inject] EntityWrapper _request;
		[Inject] IExplosionFactory _explosionFactory;

		public void Run(IEcsSystems systems)
		{
			foreach (var request in _requestFilter.Value)
			{
				_request.SetEntity(request);

				var prefab = _request.BlowUpDestructibleTile().DestructiblePrefab;
				var pos = _request.Position();
				var parent = _request.ForParent();
				_explosionFactory.CreateDestructibleTile(prefab, pos, parent);

				_request.DestroyImmediate();
			}
		}
	}
}