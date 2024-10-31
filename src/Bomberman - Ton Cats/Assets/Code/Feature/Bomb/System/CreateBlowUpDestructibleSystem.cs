using Common.Component;
using Feature.Bomb.Component;
using Feature.Bomb.Factory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine.Tilemaps;
using Zenject;

namespace Feature.Bomb.System
{
	public sealed class CreateBlowUpDestructibleSystem : IEcsRunSystem
	{
		readonly EcsFilterInject<
			Inc<CreateExplosionRequest, BlowUpDestructible, DestructibleTileCellPos,
				Position, ForParent>> _requestFilter;

		[Inject] Tilemap _tilemap;
		[Inject] EntityWrapper _request;
		[Inject] IExplosionFactory _explosionFactory;

		public void Run(IEcsSystems systems)
		{
			foreach (var request in _requestFilter.Value)
			{
				_request.SetEntity(request);

				DestroyTile();
				CreateDestructiblePrefab();

				_request.DestroyImmediate();
			}
		}

		void DestroyTile()
		{
			var cellPos = _request.DestructibleTileCellPos();
			_tilemap.SetTile(cellPos, null);
		}

		void CreateDestructiblePrefab()
		{
			var prefab = _request.BlowUpDestructibleTile().DestructiblePrefab;
			var pos = _request.Position();
			var parent = _request.ForParent();
			_explosionFactory.CreateDestructibleTile(prefab, pos, parent);
		}
	}
}