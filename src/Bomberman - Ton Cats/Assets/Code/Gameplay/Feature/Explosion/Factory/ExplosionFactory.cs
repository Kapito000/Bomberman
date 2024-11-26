using Common.Component;
using Extensions;
using Gameplay.Feature.Explosion.Behaviour;
using Gameplay.Feature.Explosion.Component;
using Infrastructure.ECS;
using Infrastructure.Factory.Kit;
using Leopotam.EcsLite;
using Unity.Mathematics;
using UnityEngine;
using Zenject;
using Transform = UnityEngine.Transform;

namespace Gameplay.Feature.Explosion.Factory
{
	public sealed class ExplosionFactory : IExplosionFactory
	{
		[Inject] EcsWorld _world;
		[Inject] IFactoryKit _kit;
		[Inject] EntityWrapper _explosion;

		public int CreateExplosionRequest(Vector2 pos)
		{
			var entity = _world.NewEntity();
			_explosion.SetEntity(entity);
			_explosion
				.Add<ExplosionRequest>()
				.Add<Position>().With(e => e.SetPosition(pos))
				;
			return entity;
		}

		public int CreateExplosionPart(Vector2 pos, Vector2 direction,
			Transform parent, EExplosionPart part)
		{
			var instance = InstantiateExplosion(pos, parent);
			var entity = InitExplosionEntity(instance);
			SetRotation(instance, direction);
			PlayAnimation(instance, part);
			return entity;
		}

		public int CreateExplosionCenter(Vector2 pos, Transform parent)
		{
			var instance = InstantiateExplosion(pos, parent);
			var entity = InitExplosionEntity(instance);
			PlayAnimation(instance, EExplosionPart.Center);
			return entity;
		}
		
		public void CreateDestructibleTile(Vector2 pos, Transform parent)
		{
			var prefab = _kit.AssetProvider.DestructibleTile();
			_kit.InstantiateService.Instantiate(prefab, pos, parent);
		}

		void PlayAnimation(GameObject instance, EExplosionPart part)
		{
			if (!instance.TryGetComponent<ExplosionAnimator>(out var animator))
			{
				Debug.LogError($"The instance has no \"{nameof(ExplosionAnimator)}\".");
				return;
			}

			PlayExplosionAnimation(animator, part);
		}

		GameObject InstantiateExplosion(Vector2 pos, Transform parent)
		{
			var prefab = _kit.AssetProvider.Explosion();
			return _kit.InstantiateService
				.Instantiate(prefab, pos, quaternion.identity, parent);
		}

		int InitExplosionEntity(GameObject instance)
		{
			var entity = _kit.EntityBehaviourFactory.InitEntityBehaviour(instance);
			_explosion.SetEntity(entity);
			_explosion
				.Add<Component.Explosion>()
				.Add<FirstBreath>()
				;
			return entity;
		}

		void SetRotation(GameObject instance, Vector2 direction)
		{
			var angle = Mathf.Atan2(direction.y, direction.x);
			instance.transform.rotation = Quaternion
				.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
		}

		void PlayExplosionAnimation(ExplosionAnimator animator, EExplosionPart part)
		{
			switch (part)
			{
				case EExplosionPart.Center:
					animator.PlayCenter();
					break;
				case EExplosionPart.Middle:
					animator.PlayMiddle();
					break;
				case EExplosionPart.End:
					animator.PlayEnd();
					break;
				default:
					Debug.LogError($"Unknown explosion part: \"{part}\".");
					return;
			}
		}
	}
}