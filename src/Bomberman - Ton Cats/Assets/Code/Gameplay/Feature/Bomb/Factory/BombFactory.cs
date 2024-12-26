using Common.Component;
using Extensions;
using Gameplay.Feature.Bomb.Behaviour;
using Gameplay.Feature.Bomb.Component;
using Gameplay.Feature.Bomb.StaticData;
using Infrastructure.ECS;
using Infrastructure.Factory.Kit;
using Infrastructure.TimeService;
using Leopotam.EcsLite;
using Unity.Mathematics;
using UnityEngine;
using Zenject;
using MappedSpan =
	System.Collections.Generic.IReadOnlyDictionary<string, float>;

namespace Gameplay.Feature.Bomb.Factory
{
	public sealed class BombFactory : IBombFactory
	{
		[Inject] EcsWorld _world;
		[Inject] IFactoryKit _kit;
		[Inject] ITimeService _timeService;
		[Inject] EntityWrapper _bomb;
		[Inject] EntityWrapper _bombParent;
		[Inject] EntityWrapper _entity;
		[Inject] IBombDataService _bombDataService;

		public int CreateBomb(BombType bombType, Vector2 pos, Transform parent)
		{
			var prefab = _kit.AssetProvider.Bomb();
			var instance = _kit.InstantiateService.Instantiate(prefab, pos, parent);
			var entity = _kit.EntityBehaviourFactory.InitEntityBehaviour(instance);
			_bomb.SetEntity(entity);
			_bomb
				.Add<BombComponent>()
				;
			InitBombType(_bomb, bombType);

			return entity;
		}

		public int CreateBombParent()
		{
			var instance = new GameObject("Bombs");
			var entity = _kit.EntityBehaviourFactory.InitEntityBehaviour(instance);
			_bombParent.SetEntity(entity);
			_bombParent
				.Add<BombParent>()
				.Add<TransformComponent>()
				.With(e => e.SetTransform(instance.transform))
				;
			return entity;
		}

		public int CreateExplosionRequest(Vector2 pos, float explosionRadius)
		{
			var entity = _world.NewEntity();
			_entity.SetEntity(entity);
			_entity
				.Add<ExplosionRequest>()
				.AddPosition(pos)
				.AddExplosionRadius(explosionRadius)
				;
			return entity;
		}

		public int CreateExplosionPart(Vector2 pos, Vector2 direction,
			Transform parent, ExplosionPart part)
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
			_entity.SetEntity(entity);
			_entity
				.Add<ExplosionCenter>()
				;
			PlayAnimation(instance, ExplosionPart.Center);
			return entity;
		}

		public void CreateDestructibleTile(Vector2 pos, Transform parent)
		{
			var prefab = _kit.AssetProvider.DestructibleTile();
			_kit.InstantiateService.Instantiate(prefab, pos, parent);
		}

		void InitBombType(EntityWrapper bomb, BombType bombType)
		{
			var successful = _bombDataService
				.TryGetCharacteristic(bombType, out var characteristics);
			if (successful == false)
			{
				Debug.LogError(
					$"Cannot to get data for the \"{bombType.ToString()}\" bomb.");
				return;
			}

			AddExplosionTimer(bomb, characteristics);
			AddExplosionRadius(bomb, characteristics);
		}

		void AddExplosionTimer(EntityWrapper bomb, MappedSpan characteristics)
		{
			var name = BombCharacteristic.ExplosionTimer.ToString();
			if (characteristics.TryGetValue(name, out var timer)
			    && timer > 0)
				bomb.AddExplosionTimer(_timeService.GameTime() + timer);
			else
				bomb.AddExplosionTimer(4);
		}

		void AddExplosionRadius(EntityWrapper bomb, MappedSpan characteristics)
		{
			var name = BombCharacteristic.ExplosionRadius.ToString();
			if (characteristics.TryGetValue(name, out var radius))
				bomb.AddExplosionRadius(radius);
			else
				bomb.AddExplosionRadius(1);
		}

		void PlayAnimation(GameObject instance, ExplosionPart part)
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
			_entity.SetEntity(entity);
			_entity
				.Add<FirstBreath>()
				.Add<Explosion>()
				.AddTransform(instance.transform)
				;
			return entity;
		}

		void SetRotation(GameObject instance, Vector2 direction)
		{
			var angle = Mathf.Atan2(direction.y, direction.x);
			instance.transform.rotation = Quaternion
				.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
		}

		void PlayExplosionAnimation(ExplosionAnimator animator, ExplosionPart part)
		{
			switch (part)
			{
				case ExplosionPart.Center:
					animator.PlayCenter();
					break;
				case ExplosionPart.Middle:
					animator.PlayMiddle();
					break;
				case ExplosionPart.End:
					animator.PlayEnd();
					break;
				default:
					Debug.LogError($"Unknown explosion part: \"{part}\".");
					return;
			}
		}
	}
}