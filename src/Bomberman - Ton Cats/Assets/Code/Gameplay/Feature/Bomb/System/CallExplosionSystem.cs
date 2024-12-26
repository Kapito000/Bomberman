using Common.Component;
using Gameplay.Feature.Bomb.Component;
using Gameplay.Feature.Bomb.Factory;
using Gameplay.Feature.Map.Component;
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

				for (int i = 0; i < explosionRadius; i++)
				{
					var part = i == explosionRadius - 1
						? ExplosionPart.End
						: ExplosionPart.Middle;

					RequestCreatingExplosion(
						cell + Vector2Int.up * i, parent, part, Vector2.up);
					RequestCreatingExplosion(
						cell + Vector2Int.down * i, parent, part, Vector2.down);
					RequestCreatingExplosion(
						cell + Vector2Int.left * i, parent, part, Vector2.left);
					RequestCreatingExplosion(
						cell + Vector2Int.right * i, parent, part, Vector2.right);
				}

				_callExplosion.Destroy();
			}
		}

		void RequestCreatingExplosion(Vector2Int cell, Transform parent,
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
		}

		Transform Parent(int bombParent)
		{
			_bombParent.SetEntity(bombParent);
			var parent = _bombParent.Transform();
			return parent;
		}
	}
}