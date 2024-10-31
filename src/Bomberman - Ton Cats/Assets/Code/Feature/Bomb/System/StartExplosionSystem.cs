using Common.Component;
using Extensions;
using Feature.Bomb.Component;
using Feature.Bomb.Factory;
using Feature.Destruction.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;
using Transform = UnityEngine.Transform;

namespace Feature.Bomb.System
{
	public sealed class StartExplosionSystem : EcsSystem, IEcsRunSystem
	{
		[Inject] EntityWrapper _bombParent;
		[Inject] EntityWrapper _createRequest;
		[Inject] EntityWrapper _explosionRequest;
		[Inject] IExplosionFactory _explosionFactory;

		readonly EcsFilterInject<Inc<ExplosionRequest, Position>> _filter;
		readonly EcsFilterInject<
			Inc<BombParent, Common.Component.Transform>> _bombParentFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var bombParent in _bombParentFilter.Value)
			foreach (var request in _filter.Value)
			{
				var parent = Parent(bombParent);
				_explosionRequest.SetEntity(request);
				Vector2 pos = _explosionRequest.Position();

				RequestCreatingExplosionCenter(pos, parent);
				RequestCreatingExplosionPart(pos + Vector2.up, Vector2.up, EExplosionPart.End, parent);
				RequestCreatingExplosionPart(pos + Vector2.down, Vector2.down, EExplosionPart.End, parent);
				RequestCreatingExplosionPart(pos + Vector2.left, Vector2.left, EExplosionPart.End, parent);
				RequestCreatingExplosionPart(pos + Vector2.right, Vector2.right, EExplosionPart.End, parent);

				_explosionRequest.Add<Destructed>();
			}
		}

		void RequestCreatingExplosionCenter(Vector2 pos, Transform parent)
		{
			RequestCreatingExplosion(pos, parent)
				.Add<ExplosionCenter>()
				;
		}

		void RequestCreatingExplosionPart(Vector2 pos, Vector2 dir,
			EExplosionPart part, Transform parent)
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