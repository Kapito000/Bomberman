using Common.Component;
using Feature.Destruction.Component;
using Feature.Explosion.Component;
using GameTileMap;
using Infrastructure.ECS;
using InstantiateService;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using MapTile;
using Zenject;

namespace Feature.Explosion.System
{
	public sealed class ScanExplosionAreaSystem : IEcsRunSystem
	{
		[Inject] IGameMap _gameMap;
		[Inject] EntityWrapper _request;
		[Inject] IInstantiateService _instantiateService;

		readonly EcsFilterInject<Inc<CreateExplosionRequest, Position>> _filter;

		public void Run(IEcsSystems systems)
		{
			foreach (var request in _filter.Value)
			{
				_request.SetEntity(request);

				var pos = _request.Position();
				var cellPos = _gameMap.WorldToCell(pos);
				if (_gameMap.TryGetTile(cellPos, out var tile) == false)
					continue;

				if (tile is IDestructible destructible)
				{
					_request
						.AddBlowUpDestructible(destructible)
						.AddDestructibleTileCellPos(cellPos)
						;
				}
				else if (tile is IIndestructible)
				{
					_request
						.Add<Destructed>()
						.Remove<CreateExplosionRequest>()
						;
				}
			}
		}
	}
}