using Common.Component;
using Feature.Bomb.Component;
using Feature.Bomb.Factory;
using Feature.Destruction.Component;
using Infrastructure.ECS;
using InstantiateService;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using MapTile;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Feature.Bomb.System
{
	public sealed class ExplosionSystem : EcsSystem, IEcsRunSystem
	{
		[Inject] Tilemap _tilemap;
		[Inject] EntityWrapper _explosionRequest;
		[Inject] IExplosionFactory _explosionFactory;
		[Inject] IInstantiateService _instantiateService;

		readonly EcsFilterInject<Inc<ExplosionRequest, Position>> _filter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _filter.Value)
			{
				_explosionRequest.SetEntity(e);
				Vector2 pos = _explosionRequest.Position();
				_explosionFactory.CreateExplosionCenter(pos, null);
				Explode(pos + Vector2.up, Vector2.up, ExplosionPart.End);
				Explode(pos + Vector2.down, Vector2.down, ExplosionPart.End);
				Explode(pos + Vector2.left, Vector2.left, ExplosionPart.End);
				Explode(pos + Vector2.right, Vector2.right, ExplosionPart.End);

				_explosionRequest.Add<Destructed>();
			}
		}

		void Explode(Vector2 pos, Vector2 direction, ExplosionPart part)
		{
			var cellPos = _tilemap.WorldToCell(pos);
			var tile = _tilemap.GetTile(cellPos);
			if (tile is Destructible destructible)
			{
				_tilemap.SetTile(cellPos, null);
				_instantiateService
					.Instantiate(destructible.DestructibleObj, pos, null);
			}
			else
			{
				_explosionFactory.CreateExplosionPart(pos, direction, null, part);
			}
		}
	}
}