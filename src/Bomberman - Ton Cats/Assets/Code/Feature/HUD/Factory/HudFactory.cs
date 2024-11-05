using Common.Component;
using Extensions;
using Factory.Kit;
using Feature.HUD.Component;
using Infrastructure.ECS;
using Zenject;
using UnityEngine;

namespace Feature.HUD.Factory
{
	public sealed class HudFactory : IHudFactory
	{
		[Inject] IFactoryKit _kit;
		[Inject] EntityWrapper _hudRootEntity;
		[Inject] EntityWrapper _entity;

		public int CreateHudRoot(Transform parent)
		{
			var hudRoot = _kit.AssetProvider.HudRoot();
			var instance = _kit.InstantiateService.Instantiate(hudRoot, parent);
			var entity = _kit.EntityBehaviourFactory.InitEntityBehaviour(instance);
			_hudRootEntity.SetEntity(entity);

			var transform = instance.GetComponent<Transform>();
			_hudRootEntity
				.Add<HudRoot>()
				.Add<TransformComponent>().With(e => e.SetTransform(transform))
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
	}
}