using Common.Component;
using Extensions;
using Feature.Bomb.Component;
using Feature.Camera;
using Feature.DamageApplication.Component;
using Feature.Enemy.AI.Blackboard;
using Feature.Enemy.Base.Component;
using Feature.Explosion;
using Feature.Explosion.Component;
using Feature.Hero.Behaviour;
using Feature.Hero.Component;
using Feature.HUD.Behaviour;
using Feature.HUD.Component;
using Feature.Life.Component;
using Leopotam.EcsLite;
using MapTile;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.U2D.Animation;
using Rigidbody2D = UnityEngine.Rigidbody2D;
using Transform = UnityEngine.Transform;

namespace Infrastructure.ECS
{
	public partial class EntityWrapper
	{
		public EcsWorld World() => _world;

		public Transform Transform() =>
			_world.Transform(_entity);

		public EntityWrapper AddTransform(Transform transform)
		{
			ref var transformComponent = ref AddComponent<TransformComponent>();
			transformComponent.Value = transform;
			return this;
		}

		public Vector3 TransformPos() =>
			_world.TransformPos(_entity);

		public EntityWrapper SetTransform(Transform transform)
		{
			_world.SetTransform(_entity, transform);
			return this;
		}

		public void SetRigidbody2DVelocity(Vector2 velocity)
		{
			var rb = Rigidbody2D();
			rb.velocity = velocity;
		}

		public Rigidbody2D Rigidbody2D()
		{
			ref var rigidbody2D = ref _world
				.GetPool<Rigidbody2DComponent>()
				.Get(_entity);

			return rigidbody2D.Value;
		}

		public Vector2 MoveDirection()
		{
			ref var moveDirection =
				ref _world.GetPool<MovementDirection>().Get(_entity);
			return moveDirection.Value;
		}

		public float MoveSpeed()
		{
			ref var speed = ref _world.GetPool<MoveSpeed>().Get(_entity);
			return speed.Value;
		}

		public void SetMoveSpeed(float value)
		{
			ref var speed = ref _world.GetPool<MoveSpeed>().Get(_entity);
			speed.Value = value;
		}

		public void AddVirtualCameraFollowTarget(Transform followTarget) =>
			_world.AddFollowTarget(_entity, followTarget);

		public void SetBombNumber(int value)
		{
			ref var bombNumber = ref _world.GetPool<BombNumber>().Get(_entity);
			bombNumber.Value = value;
		}

		public int BombNumber()
		{
			ref var bombNumber = ref Get<BombNumber>();
			return bombNumber.Value;
		}

		public IEntityView View()
		{
			ref var view = ref Get<View>();
			return view.Value;
		}

		public Vector3 Position()
		{
			ref var position = ref _world.GetPool<Position>().Get(_entity);
			return position.Value;
		}

		public void SetPosition(Vector2 pos)
		{
			ref var position = ref Get<Position>();
			position.Value = pos;
		}

		public Vector2 Direction()
		{
			ref var dir = ref Get<Direction>();
			return dir.Value;
		}

		public EntityWrapper AddDirection(Vector2 dir)
		{
			ref var direction = ref AddComponent<Direction>();
			direction.Value = dir;
			return this;
		}

		public void SetDirection(Vector2 dir)
		{
			ref var direction = ref Get<Direction>();
			direction.Value = dir;
		}

		public int LifePoints()
		{
			ref var health = ref Get<LifePoints>();
			return health.Value;
		}

		public EntityWrapper SetLifePoints(int value)
		{
			ref var health = ref Get<LifePoints>();
			health.Value = value;
			return this;
		}

		public IDestructible BlowUpDestructibleTile()
		{
			ref var blowUpDestructible = ref Get<BlowUpDestructible>();
			return blowUpDestructible.Destructible;
		}

		public EExplosionPart ExplosionPart()
		{
			ref var explosionPart = ref Get<ExplosionPart>();
			return explosionPart.Value;
		}

		public EntityWrapper AddExplosionPart(EExplosionPart part)
		{
			ref var explosionPart = ref AddComponent<ExplosionPart>();
			explosionPart.Value = part;
			return this;
		}

		public EntityWrapper AddBlowUpDestructible(IDestructible destructible)
		{
			ref var blowUpDestructible = ref AddComponent<BlowUpDestructible>();
			blowUpDestructible.Destructible = destructible;
			return this;
		}

		public Vector3Int DestructibleTileCellPos()
		{
			ref var destructibleTile = ref Get<DestructibleTileCellPos>();
			return destructibleTile.Value;
		}

		public EntityWrapper AddDestructibleTileCellPos(Vector3Int cellPos)
		{
			ref var destructibleTilePos = ref AddComponent<DestructibleTileCellPos>();
			destructibleTilePos.Value = cellPos;
			return this;
		}

		public EntityWrapper SetBlowUpDestructible(IDestructible destructible)
		{
			ref var blowUpDestructible = ref Get<BlowUpDestructible>();
			blowUpDestructible.Destructible = destructible;
			return this;
		}

		public Transform ForParent()
		{
			ref var forParent = ref Get<ForParent>();
			return forParent.Value;
		}

		public EntityWrapper AddForParent(Transform parent)
		{
			ref var forParent = ref AddComponent<ForParent>();
			forParent.Value = parent;
			return this;
		}

		public int Damage()
		{
			ref var damage = ref Get<Damage>();
			return damage.Value;
		}

		public EntityWrapper ReplaceDamage(int value)
		{
			ref var damage = ref ReplaceComponent<Damage>();
			damage.Value = value;
			return this;
		}

		public EntityWrapper AppendDamage(int value)
		{
			ref var damage = ref ReplaceComponent<Damage>();
			damage.Value += value;
			return this;
		}

		public LifePointsPanel LifePointsPanel()
		{
			ref var component = ref Get<LifePointsPanelComponent>();
			return component.Value;
		}

		public EntityWrapper AddLifePointsPanel(LifePointsPanel value)
		{
			ref var lifePointsPanel = ref AddComponent<LifePointsPanelComponent>();
			lifePointsPanel.Value = value;
			return this;
		}

		public BombCounterPanel BombCounterPanel()
		{
			ref var bombCounterPanel = ref Get<BombCounterPanelComponent>();
			return bombCounterPanel.Value;
		}

		public EntityWrapper AddBombCounterPanel(BombCounterPanel value)
		{
			ref var bombCounterPanel = ref AddComponent<BombCounterPanelComponent>();
			bombCounterPanel.Value = value;
			return this;
		}

		public HeroAnimator HeroAnimator()
		{
			ref var heroAnimatorComponent = ref Get<HeroAnimatorComponent>();
			return heroAnimatorComponent.Value;
		}

		public void AddHeroAnimator(HeroAnimator heroAnimator)
		{
			ref var component = ref AddComponent<HeroAnimatorComponent>();
			component.Value = heroAnimator;
		}

		public EntityWrapper IsMoving(bool value)
		{
			if (value)
				ReplaceComponent<Moving>();
			else
				Remove<Moving>();

			return this;
		}

		public bool IsMoving() =>
			Has<Moving>();

		public SpriteLibrary SpriteLibrary()
		{
			ref var spriteLibraryComponent = ref Get<SpriteLibraryComponent>();
			return spriteLibraryComponent.Value;
		}

		public BaseEnemyAIBlackboard BaseEnemyAIBlackboard()
		{
			ref var blackboard = ref Get<BaseEnemyAIBlackboardComponent>();
			return blackboard.Value;
		}

		public EntityWrapper AddBaseEnemyAIBlackboard()
		{
			ref var blackboard = ref AddComponent<BaseEnemyAIBlackboardComponent>();
			blackboard.Value = new BaseEnemyAIBlackboard();
			return this;
		}

		public NavMeshAgent NavMeshAgent()
		{
			ref var navMeshAgent = ref Get<NavMeshAgentComponent>();
			return navMeshAgent.Value;
		}

		public int ChangeLifePoints()
		{
			ref var changeLifePoints = ref Get<ChangeLifePoints>();
			return changeLifePoints.Value;
		}

		public EntityWrapper ReplaceChangeLifePoints(int value)
		{
			ref var changeLifePoints = ref ReplaceComponent<ChangeLifePoints>();
			changeLifePoints.Value = value;
			return this;
		}

		public EntityWrapper AppendChangeLifePoints(int value)
		{
			ref var changeLifePoints = ref ReplaceComponent<ChangeLifePoints>();
			changeLifePoints.Value += value;
			return this;
		}

		public EntityWrapper AddTakenDamageEffectDuration(float duration)
		{
			ref var effectDuration = ref AddComponent<TakenDamageEffectDuration>();
			effectDuration.Value = duration;
			return this;
		}

		public float TakenDamageEffectDuration()
		{
			ref var effectDuration = ref Get<TakenDamageEffectDuration>();
			return effectDuration.Value;
		}

		public EntityWrapper SetTakenDamageEffectDuration(float value)
		{
			ref var effectDuration = ref Get<TakenDamageEffectDuration>();
			effectDuration.Value = value;
			return this;
		}

		public SpriteRenderer SpriteRenderer()
		{
			ref var spriteRendererComponent = ref Get<SpriteRendererComponent>();
			return spriteRendererComponent.Value;
		}
	}
}