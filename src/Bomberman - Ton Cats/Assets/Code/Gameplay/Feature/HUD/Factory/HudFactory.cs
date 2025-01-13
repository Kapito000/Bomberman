using Common.Component;
using Common.HUD;
using Extensions;
using Gameplay.Feature.HUD.Component;
using Infrastructure.ECS;
using Infrastructure.Factory.Kit;
using UnityEngine;
using Zenject;
using GameTimerDisplay =
	Gameplay.Feature.HUD.Feature.Timer.Behaviour.GameTimerDisplay;
using NotImplementedException = System.NotImplementedException;

namespace Gameplay.Feature.HUD.Factory
{
	public sealed class HudFactory : IHudFactory
	{
		[Inject] IFactoryKit _kit;
		[Inject] EntityWrapper _hudRootEntity;
		[Inject] EntityWrapper _entity;

		public int CreateHudRoot(Transform parent)
		{
			var hudRoot = _kit.AssetProvider.HudRoot();
			var instance = _kit.InstantiateService
				.Instantiate<Canvas>(hudRoot, parent);
			var entity = _kit.EntityBehaviourFactory
				.InitEntityBehaviour(instance.gameObject);
			_hudRootEntity.SetEntity(entity);

			var transform = instance.GetComponent<Transform>();
			_hudRootEntity
				.Add<HudRoot>()
				.Add<TransformComponent>().With(e => e.SetTransform(transform))
				.AddCanvas(instance)
				;
			return entity;
		}

		public void CreateCharacterJoystick(Transform parent)
		{
			var characterJoystick = _kit.AssetProvider.CharacterJoystick();
			var instance =
				_kit.InstantiateService.Instantiate(characterJoystick, parent);
		}

		public void CreatePutBombButton(Transform parent)
		{
			var prefab = _kit.AssetProvider.PutBombButton();
			var instance = _kit.InstantiateService.Instantiate(prefab, parent);
		}

		public int CreateUpperPanel(Transform parent)
		{
			var prefab = _kit.AssetProvider.UpperPanel();
			var instance = _kit.InstantiateService.Instantiate(prefab, parent);
			var e = _kit.EntityBehaviourFactory.InitEntityBehaviour(instance);
			_entity.SetEntity(e);
			_entity
				.Add<UpperPanel>()
				.AddTransform(instance.transform);
			;
			return e;
		}

		public int CreateTimerDisplay(Transform parent)
		{
			var prefab = _kit.AssetProvider.GameTimerDisplay();
			var instance = _kit.InstantiateService
				.Instantiate<GameTimerDisplay>(prefab, parent);
			var e = _kit.EntityBehaviourFactory.InitEntityBehaviour(instance
				.gameObject);
			_entity.SetEntity(e);
			_entity
				.AddGameTimerDisplay(instance)
				;
			return e;
		}

		public int CreateLifePointsPanel(Transform parent)
		{
			var prefab = _kit.AssetProvider.LifePointsPanel();
			var instance = _kit.InstantiateService
				.Instantiate<IntegerDisplay>(prefab, parent);
			var e = _kit.EntityBehaviourFactory
				.InitEntityBehaviour(instance.gameObject);
			_entity.SetEntity(e);
			_entity
				.AddTransform(instance.transform)
				.AddLifePointsPanel(instance)
				;
			return e;
		}

		public int CreateBombCounterPanel(Transform parent)
		{
			var prefab = _kit.AssetProvider.BombCounterPanel();
			var instance = _kit.InstantiateService
				.Instantiate<IntegerDisplay>(prefab, parent);
			var e = _kit.EntityBehaviourFactory
				.InitEntityBehaviour(instance.gameObject);
			_entity.SetEntity(e);
			_entity
				.AddTransform(instance.transform)
				.AddBombCounterPanel(instance)
				;

			return e;
		}

		public int CreateEnemyCounterDisplay(Transform parent)
		{
			var prefab = _kit.AssetProvider.EnemyCounterDisplay();
			var instance = _kit.InstantiateService
				.Instantiate<IntegerDisplay>(prefab, parent);
			var e = _kit.EntityBehaviourFactory
				.InitEntityBehaviour(instance.gameObject);
			_entity.SetEntity(e);
			_entity
				.AddTransform(instance.transform)
				.AddEnemyCounterDisplay(instance)
				;
			return e;
		}
	}
}