using Common.Component;
using Extensions;
using Gameplay.Feature.Bomb.Behaviour;
using Gameplay.Feature.Bomb.Component;
using Gameplay.Feature.Bomb.StaticData;
using Gameplay.Feature.Map.MapController;
using Infrastructure.ECS;
using Infrastructure.Factory.Kit;
using Infrastructure.TimeService;
using Leopotam.EcsLite;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using MappedSpan =
	System.Collections.Generic.IReadOnlyDictionary<string, float>;
using Vector2 = UnityEngine.Vector2;

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
		[Inject] IMapController _mapController;
		[Inject] IBombDataService _bombDataService;

		public int CreateBomb(BombType bombType, Vector2Int cell, Transform parent)
		{
			var prefab = _kit.AssetProvider.Bomb(bombType);
			var pos = _mapController.GetCellCenterWorld(cell);
			var instance = _kit.InstantiateService.Instantiate(prefab, pos, parent);
			var entity = _kit.EntityBehaviourFactory.InitEntityBehaviour(instance);
			_bomb.SetEntity(entity);
			_bomb
				.Add<BombComponent>()
				;
			InitBombType(_bomb, bombType);
			TryInitBombHunter(bombType, _bomb, instance);

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

		public int CreateCallExplosion(Vector2Int cell, int explosionRadius)
		{
			var entity = _world.NewEntity();
			_entity.SetEntity(entity);
			_entity
				.Add<CallExplosion>()
				.AddCellPos(cell)
				.AddExplosionRadius(explosionRadius)
				;
			return entity;
		}

		public int CreateExplosionPart(Vector2Int cell, ExplosionPart part,
			Transform parent, Vector2 direction = default)
		{
			var pos = _mapController.GetCellCenterWorld(cell);
			var instance = InstantiateExplosion(pos, parent);
			var e = _kit.EntityBehaviourFactory.InitEntityBehaviour(instance);
			_entity.SetEntity(e);
			_entity
				.Add<FirstBreath>()
				.Add<Explosion>()
				.AddTransform(instance.transform)
				;

			if (part != ExplosionPart.Center)
				SetRotation(instance, direction);
			PlayAnimation(instance, part);
			return e;
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

		bool TryInitBombHunter(BombType bombType, EntityWrapper bomb,
			GameObject obj)
		{
			if (bombType != BombType.Hunter)
				return false;

			bomb.Add<BombHunter>();

			if (obj.TryGetComponent<NavMeshAgent>(out var navMeshAgent) == false)
			{
				Debug.LogError($"Cannot to get \"{nameof(NavMeshAgent)}\"");
				return false;
			}

			int mask = 1 << Constant.NavMesh.AreaMask.Walkable;
			if (NavMesh.SamplePosition(obj.transform.position, out _, .1f, mask))
			{
				navMeshAgent.enabled = true;
			}

			return true;
		}

		void AddExplosionTimer(EntityWrapper bomb, MappedSpan characteristics)
		{
			var name = BombCharacteristic.ExplosionTimer.ToString();
			if (characteristics.TryGetValue(name, out var timer)
			    && timer > 0)
				bomb.AddExplosionTimer(_timeService.GameTime() + timer);
		}

		void AddExplosionRadius(EntityWrapper bomb, MappedSpan characteristics)
		{
			var name = BombCharacteristic.ExplosionRadius.ToString();
			if (characteristics.TryGetValue(name, out var radius))
				bomb.AddExplosionRadius((int)radius);
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