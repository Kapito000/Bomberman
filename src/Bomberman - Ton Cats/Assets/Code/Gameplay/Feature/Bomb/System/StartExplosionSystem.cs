using Common.Component;
using Extensions;
using Gameplay.Feature.Bomb.Component;
using Gameplay.Feature.Bomb.Factory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;
using Transform = UnityEngine.Transform;

namespace Gameplay.Feature.Bomb.System
{
	public sealed class StartExplosionSystem : IEcsRunSystem
	{
		[Inject] EcsWorld _world;
		[Inject] EntityWrapper _bombParent;
		[Inject] EntityWrapper _createRequest;
		[Inject] EntityWrapper _explosionRequest;
		[Inject] IBombFactory _factory;

		readonly EcsFilterInject<Inc<ExplosionRequest, Position>> _filter;
		readonly EcsFilterInject<
			Inc<BombParent, TransformComponent>> _bombParentFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var bombParent in _bombParentFilter.Value)
			foreach (var request in _filter.Value)
			{
				var parent = Parent(bombParent);
				_explosionRequest.SetEntity(request);
				Vector2 pos = _explosionRequest.Position();

				RequestCreatingExplosionCenter(pos, parent);
				RequestCreatingExplosionPart(pos + Vector2.up, Vector2.up, ExplosionPart.End, parent);
				RequestCreatingExplosionPart(pos + Vector2.down, Vector2.down, ExplosionPart.End, parent);
				RequestCreatingExplosionPart(pos + Vector2.left, Vector2.left, ExplosionPart.End, parent);
				RequestCreatingExplosionPart(pos + Vector2.right, Vector2.right, ExplosionPart.End, parent);

				_explosionRequest.Destroy();
			}
		}

		void RequestCreatingExplosionCenter(Vector2 pos, Transform parent)
		{
			RequestCreatingExplosion(pos, parent)
				.Add<ExplosionCenter>()
				;
		}

		void RequestCreatingExplosionPart(Vector2 pos, Vector2 dir,
			ExplosionPart part, Transform parent)
		{
			RequestCreatingExplosion(pos, parent)
				.AddDirection(dir)
				.AddExplosionPart(part)
				;
		}

		EntityWrapper RequestCreatingExplosion(Vector2 pos, Transform parent)
		{
			_createRequest.SetEntity(_world.NewEntity());
			_createRequest
				.Add<CreateExplosionRequest>()
				.Add<Position>().With(e => e.SetPosition(pos))
				.AddForParent(parent)
				;
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