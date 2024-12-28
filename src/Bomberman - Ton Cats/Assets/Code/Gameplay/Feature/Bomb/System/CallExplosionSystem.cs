using Common.Component;
using Gameplay.Feature.Bomb.Component;
using Gameplay.Feature.Bomb.Factory;
using Gameplay.Feature.Map.Component;
using Gameplay.Feature.Map.MapController;
using Gameplay.Map;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;
using Transform = UnityEngine.Transform;

namespace Gameplay.Feature.Bomb.System
{
	public sealed class CallExplosionSystem : IEcsRunSystem
	{
		[Inject] EcsWorld _world;
		[Inject] IBombFactory _factory;
		[Inject] EntityWrapper _bombParent;
		[Inject] EntityWrapper _createRequest;
		[Inject] EntityWrapper _callExplosion;
		[Inject] IMapController _mapController;

		readonly EcsFilterInject<Inc<CallExplosion, CellPos, ExplosionRadius>>
			_callExplosionFilter;
		readonly EcsFilterInject<
			Inc<BombParent, TransformComponent>> _bombParentFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var bombParentEntity in _bombParentFilter.Value)
			foreach (var requestEntity in _callExplosionFilter.Value)
			{
				_callExplosion.SetEntity(requestEntity);

				var cell = _callExplosion.CellPos();
				var explosionRadius = _callExplosion.ExplosionRadius();

				var parent = Parent(bombParentEntity);
				RequestCreatingExplosion(cell, parent, ExplosionPart.Center);


				if (explosionRadius > 0)
				{
					RequestForDirection(explosionRadius, parent, cell, Vector2Int.up);
					RequestForDirection(explosionRadius, parent, cell, Vector2Int.down);
					RequestForDirection(explosionRadius, parent, cell, Vector2Int.left);
					RequestForDirection(explosionRadius, parent, cell, Vector2Int.right);
				}

				_callExplosion.Destroy();
			}
		}

		void RequestForDirection(int explosionRadius, Transform parent,
			Vector2Int center, Vector2Int dir)
		{
			for (int i = 0; i < explosionRadius; i++)
			{
				var part = i == explosionRadius - 1
					? ExplosionPart.End
					: ExplosionPart.Middle;

				var cell = center + dir * (i + 1);

				if (_mapController.TryGet(cell, out TileType tileType)
				    && tileType == TileType.Indestructible)
					break;

				if (tileType == TileType.Destructible)
				{
					var request = RequestCreatingExplosion(cell, parent, part, dir);
					request.Add<BlowUpDestructible>();
					break;
				}

				RequestCreatingExplosion(cell, parent, part, dir);
			}
		}

		EntityWrapper RequestCreatingExplosion(Vector2Int cell, Transform parent,
			ExplosionPart part, Vector2 dir = default)
		{
			_createRequest.NewEntity()
				.Add<CreateExplosionRequest>()
				.AddParent(parent)
				.AddCellPos(cell)
				.AddExplosionPart(part)
				;

			if (part != ExplosionPart.Center)
				_createRequest.AddDirection(dir);

			return _createRequest;
		}

		Transform Parent(int bombParent)
		{
			_bombParent.SetEntity(bombParent);
			var parent = _bombParent.Transform();
			return parent;
		}
	}
}