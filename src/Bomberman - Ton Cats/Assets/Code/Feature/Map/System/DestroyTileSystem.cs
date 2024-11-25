using Feature.Map.Component;
using GameTileMap;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Feature.Map.System
{
	public sealed class DestroyTileSystem : IEcsRunSystem
	{
		[Inject] EcsWorld _world;
		[Inject] IGameMap _gameMap;
		[Inject] EntityWrapper _destroyRequest;

		readonly EcsFilterInject<Inc<DestroyedTile, TilePos>> _destroyedTileFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _destroyedTileFilter.Value)
			{
				_destroyRequest.SetEntity(e);
				var cellPos = _destroyRequest.TilePos();
				_gameMap.SetFree(cellPos);
			}
		}
	}
}