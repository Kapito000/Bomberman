﻿using Common.Component;
using Feature.Destruction.Component;
using Feature.Explosion.Component;
using Feature.Explosion.Factory;
using Feature.Map.MapController;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;

namespace Feature.Explosion.System
{
	public sealed class CreateBlowUpDestructibleSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _request;
		[Inject] IMapController _mapController;
		[Inject] IExplosionFactory _explosionFactory;

		readonly EcsFilterInject<
			Inc<CreateExplosionRequest, BlowUpDestructible, DestructibleTileCellPos,
				Position, ForParent>> _requestFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var request in _requestFilter.Value)
			{
				_request.SetEntity(request);

				DestroyTile(_request);
				CreateDestructiblePrefab();

				_request
					.Add<Destructed>()
					.Remove<CreateExplosionRequest>()
					;
			}
		}

		void DestroyTile(EntityWrapper request)
		{
			Vector2Int cellPos = request.DestructibleTileCellPos();
			_mapController.DestroyTile(cellPos);
		}

		void CreateDestructiblePrefab()
		{
			var pos = _request.Position();
			var parent = _request.ForParent();
			_explosionFactory.CreateDestructibleTile(pos, parent);
		}
	}
}