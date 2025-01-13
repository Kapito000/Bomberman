using Common.Collections;
using Common.Component;
using Common.HUD;
using Extensions;
using Gameplay.Feature.Audio.Behaviour;
using Gameplay.Feature.Audio.Component;
using Gameplay.Feature.Bomb;
using Gameplay.Feature.Bomb.Component;
using Gameplay.Feature.Camera;
using Gameplay.Feature.DamageApplication.Component;
using Gameplay.Feature.Enemy.AI.Blackboard;
using Gameplay.Feature.Enemy.Base.Component;
using Gameplay.Feature.Enemy.Component;
using Gameplay.Feature.Hero.Behaviour;
using Gameplay.Feature.Hero.Component;
using Gameplay.Feature.HUD.Component;
using Gameplay.Feature.Input.Component;
using Gameplay.Feature.Life.Component;
using Gameplay.Feature.Map.Component;
using Gameplay.Feature.Timer.Component;
using Gameplay.Navigation.Component;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.U2D.Animation;
using BombCounterPanel = Gameplay.Feature.HUD.Component.BombCounterPanel;
using LifePointsPanel = Gameplay.Feature.HUD.Component.LifePointsPanel;
using Rigidbody2D = UnityEngine.Rigidbody2D;
using Transform = UnityEngine.Transform;
using Vector2 = UnityEngine.Vector2;

namespace Infrastructure.ECS
{
	public partial struct EntityWrapper
	{
		public EcsWorld World() => _world;

		public bool Has<T1, T2>()
			where T1 : struct
			where T2 : struct =>
			Has(typeof(T1), typeof(T2));

		public bool Has<T1, T2, T3>()
			where T1 : struct
			where T2 : struct
			where T3 : struct =>
			Has(typeof(T1), typeof(T2), typeof(T3));

		public bool Has<T1, T2, T3, T4>()
			where T1 : struct
			where T2 : struct
			where T3 : struct
			where T4 : struct =>
			Has(typeof(T1), typeof(T2), typeof(T3), typeof(T4));

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

		public EntityWrapper AddPosition(Vector2 value)
		{
			ref var position = ref AddComponent<Position>();
			position.Value = value;
			return this;
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

		public EntityWrapper AddLifePoints(int points)
		{
			ref var component = ref AddComponent<LifePoints>();
			component.Value = points;
			return this;
		}

		public EntityWrapper SetLifePoints(int value)
		{
			ref var health = ref Get<LifePoints>();
			health.Value = value;
			return this;
		}

		public ExplosionPart ExplosionPart()
		{
			ref var explosionPart = ref Get<ExplosionPartComponent>();
			return explosionPart.Value;
		}

		public EntityWrapper AddExplosionPart(ExplosionPart part)
		{
			ref var explosionPart = ref AddComponent<ExplosionPartComponent>();
			explosionPart.Value = part;
			return this;
		}

		public Transform Parent()
		{
			ref var forParent = ref Get<Parent>();
			return forParent.Value;
		}

		public EntityWrapper AddParent(Transform parent)
		{
			ref var forParent = ref AddComponent<Parent>();
			forParent.Value = parent;
			return this;
		}

		public int Damage()
		{
			ref var damage = ref Get<Damage>();
			return damage.Value;
		}

		public EntityWrapper AppendDamage(int value)
		{
			ref var damage = ref ReplaceComponent<Damage>();
			damage.Value += value;
			return this;
		}

		public IDisplay<int> LifePointsPanel()
		{
			ref var component = ref Get<LifePointsPanel>();
			return component.Value;
		}

		public EntityWrapper AddLifePointsPanel(IDisplay<int> value)
		{
			ref var lifePointsPanel = ref AddComponent<LifePointsPanel>();
			lifePointsPanel.Value = value;
			return this;
		}

		public IDisplay<int> BombCounterPanel()
		{
			ref var bombCounterPanel = ref Get<BombCounterPanel>();
			return bombCounterPanel.Value;
		}

		public EntityWrapper AddBombCounterPanel(IDisplay<int> value)
		{
			ref var bombCounterPanel = ref AddComponent<BombCounterPanel>();
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

		public EnemyAIBlackboard BaseEnemyAIBlackboard()
		{
			ref var blackboard = ref Get<EnemyAIBlackboardComponent>();
			return blackboard.Value;
		}

		public EntityWrapper AddEnemyAIBlackboard()
		{
			ref var blackboard = ref AddComponent<EnemyAIBlackboardComponent>();
			blackboard.Value = new EnemyAIBlackboard();
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

		public Vector2Int CellPos()
		{
			ref var tilePos = ref Get<CellPos>();
			return tilePos.Value;
		}

		public EntityWrapper AddCellPos(Vector2Int pos)
		{
			ref var tilePos = ref AddComponent<CellPos>();
			tilePos.Value = pos;
			return this;
		}

		public EntityWrapper AddEnemyParent(Transform value)
		{
			ref var enemyPatent = ref AddComponent<EnemyParent>();
			enemyPatent.Value = value;
			return this;
		}

		public Transform EnemyParent()
		{
			ref var enemyPatent = ref Get<EnemyParent>();
			return enemyPatent.Value;
		}

		public float GameTimer()
		{
			ref var gameTimer = ref Get<GameTimer>();
			return gameTimer.Value;
		}

		public EntityWrapper AddGameTimer(float value)
		{
			ref var gameTimer = ref AddComponent<GameTimer>();
			gameTimer.Value = value;
			return this;
		}

		public EntityWrapper SetGameTimer(float value)
		{
			ref var gameTimer = ref Get<GameTimer>();
			gameTimer.Value = value;
			return this;
		}

		public Gameplay.Feature.HUD.Feature.Timer.Behaviour.GameTimerDisplay
			GameTimerDisplay()
		{
			ref var component = ref Get<GameTimerDisplayComponent>();
			return component.Value;
		}

		public EntityWrapper AddGameTimerDisplay(
			Gameplay.Feature.HUD.Feature.Timer.Behaviour.GameTimerDisplay
				gameTimerDisplay)
		{
			ref var component = ref AddComponent<GameTimerDisplayComponent>();
			component.Value = gameTimerDisplay;
			return this;
		}

		public AudioSource AudioSource()
		{
			ref var audioSourceComponent = ref Get<AudioSourceComponent>();
			return audioSourceComponent.Value;
		}

		public EntityWrapper AddAudioSource(AudioSource audioSource)
		{
			ref var audioSourceComponent = ref AddComponent<AudioSourceComponent>();
			audioSourceComponent.Value = audioSource;
			return this;
		}

		public string TakenDamageAudioEffectId()
		{
			ref var source = ref Get<TakenDamageAudioEffectId>();
			return source.Value;
		}

		public EntityWrapper AddTakenDamageAudioEffectId(string id)
		{
			ref var source = ref AddComponent<TakenDamageAudioEffectId>();
			source.Value = id;
			return this;
		}

		public AudioSource TakenDamageEffectAudiosSource()
		{
			ref var source = ref Get<TakenDamageEffectAudioSource>();
			return source.Value;
		}

		public EntityWrapper AddTakenDamageEffectAudioSource(
			AudioSource audioSource)
		{
			ref var source = ref AddComponent<TakenDamageEffectAudioSource>();
			source.Value = audioSource;
			return this;
		}

		public Transform AdditionalAudioSourceParent()
		{
			ref var parent = ref Get<AdditionalAudioSourceParent>();
			return parent.Value;
		}

		public EntityWrapper AddAdditionalAudioSourceParent(Transform value)
		{
			ref var parent = ref AddComponent<AdditionalAudioSourceParent>();
			parent.Value = value;
			return this;
		}

		public EntityWrapper ReplaceAdditionalAudioSourceParent(Transform value)
		{
			ref var parent = ref ReplaceComponent<AdditionalAudioSourceParent>();
			parent.Value = value;
			return this;
		}

		public string DeathAudioEffectClipId()
		{
			ref var deathAudioEffectId = ref Get<DeathAudioEffectClipId>();
			return deathAudioEffectId.Value;
		}

		public EntityWrapper AddDeathAudioEffectClipId(string value)
		{
			ref var deathAudioEffectId = ref AddComponent<DeathAudioEffectClipId>();
			deathAudioEffectId.Value = value;
			return this;
		}

		public PooledAudioSource.Pool PooledAudioSourcePool()
		{
			ref var component = ref Get<PooledAudioSourcePool>();
			return component.Value;
		}

		public EntityWrapper AddPooledAudioSourcePool(PooledAudioSource.Pool pool)
		{
			ref var component = ref AddComponent<PooledAudioSourcePool>();
			component.Value = pool;
			return this;
		}

		public PooledAudioSource PooledAudioSource()
		{
			ref var pooledItem = ref Get<PooledAudioSourceComponent>();
			return pooledItem.Value;
		}

		public EntityWrapper AddPooledAudioSourceComponent(PooledAudioSource value)
		{
			ref var component = ref AddComponent<PooledAudioSourceComponent>();
			component.Value = value;
			return this;
		}

		public string EnemyId()
		{
			ref var component = ref Get<EnemyId>();
			return component.Value;
		}

		public EntityWrapper AddEnemyId(string enemyId)
		{
			ref var component = ref AddComponent<EnemyId>();
			component.Value = enemyId;
			return this;
		}

		public float ImmortalTimer()
		{
			ref var component = ref Get<ImmortalTimer>();
			return component.Value;
		}

		public EntityWrapper AddImmortalTimer(float time)
		{
			ref var component = ref AddComponent<ImmortalTimer>();
			component.Value = time;
			return this;
		}

		public int RestoreLifePoints()
		{
			ref var component = ref Get<RestoreLifePoints>();
			return component.Value;
		}

		public EntityWrapper AddRestoreLifePoints(int lifePoints)
		{
			ref var component = ref AddComponent<RestoreLifePoints>();
			component.Value = lifePoints;
			return this;
		}

		public int EnemyCount()
		{
			ref var component = ref Get<EnemyCount>();
			return component.Value;
		}

		public EntityWrapper SetEnemyCount(int value)
		{
			ref var component = ref Get<EnemyCount>();
			component.Value = value;
			return this;
		}

		public EntityWrapper AddEnemyCounterDisplay(IDisplay<int> value)
		{
			ref var component = ref AddComponent<EnemyCounterDisplay>();
			component.Value = value;
			return this;
		}

		public IDisplay<int> EnemyCounterDisplay()
		{
			ref var component = ref Get<EnemyCounterDisplay>();
			return component.Value;
		}

		public EntityWrapper AddBombStack(int capacity)
		{
			ref var component = ref AddComponent<BombStack>();
			component.Value = new StackList<BombType>(capacity);
			return this;
		}

		public StackList<BombType> BombStack()
		{
			ref var component = ref Get<BombStack>();
			return component.Value;
		}

		public float ExplosionTimer()
		{
			ref var component = ref Get<ExplosionTimer>();
			return component.Value;
		}

		public EntityWrapper AddExplosionTimer(float value)
		{
			ref var component = ref AddComponent<ExplosionTimer>();
			component.Value = value;
			return this;
		}

		public int ExplosionRadius()
		{
			ref var component = ref Get<ExplosionRadius>();
			return component.Value;
		}

		public EntityWrapper AddExplosionRadius(int radius)
		{
			ref var component = ref AddComponent<ExplosionRadius>();
			component.Value = (int)radius;
			return this;
		}

		public Transform BombHunterTarget()
		{
			ref var component = ref AddComponent<BombHunterTarget>();
			return component.Value;
		}

		public EntityWrapper AddBombHunterTarget(Transform target)
		{
			ref var component = ref AddComponent<BombHunterTarget>();
			component.Value = target;
			return this;
		}

		public EntityWrapper ReplaceBombHunterTarget(Transform target)
		{
			ref var component = ref ReplaceComponent<BombHunterTarget>();
			component.Value = target;
			return this;
		}

		public Vector2 AgentDestination()
		{
			ref var currentPatrolPoint = ref Get<AgentDestination>();
			return currentPatrolPoint.Value;
		}

		public EntityWrapper ReplaceAgentDestination(Vector2 destination)
		{
			ref var currentPatrolPoint = ref ReplaceComponent<AgentDestination>();
			currentPatrolPoint.Value = destination;
			return this;
		}

		public Vector2 ScreenTap()
		{
			ref var component = ref Get<ScreenTap>();
			return component.Value;
		}

		public EntityWrapper AddScreenTap(Vector2 screenPos)
		{
			ref var component = ref AddComponent<ScreenTap>();
			component.Value = screenPos;
			return this;
		}

		public Camera Camera()
		{
			ref var component = ref Get<CameraComponent>();
			return component.Value;
		}
		
		public EntityWrapper AddCamera(Camera value)
		{
			ref var component = ref AddComponent<CameraComponent>();
			component.Value = value;
			return this;
		}

		public EntityWrapper AddCanvas(Canvas canvas)
		{
			ref var component = ref AddComponent<CanvasComponent>();
			component.Value = canvas;
			return this;
		}
		
		public Canvas Canvas()
		{
			ref var component = ref Get<CanvasComponent>();
			return component.Value;
		}
	}
}