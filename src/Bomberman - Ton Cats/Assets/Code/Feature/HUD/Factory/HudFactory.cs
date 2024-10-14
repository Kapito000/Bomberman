using Extensions;
using Factory.Kit;
using Feature.HUD.Component;
using Infrastructure.ECS;
using Zenject;
using Transform = UnityEngine.Transform;

namespace Feature.HUD.Factory
{
	public sealed class HudFactory : IHudFactory
	{
		[Inject] IFactoryKit _kit;
		[Inject] EntityWrapper _hudRootEntity;

		public int CreateHudRoot(Transform parent)
		{
			var hudRoot = _kit.AssetProvider.HudRoot();
			var instance = _kit.InstantiateService.Instantiate(hudRoot, parent);
			var entity = _kit.EntityBehaviourFactory.InitEntityBehaviour(instance);
			_hudRootEntity.SetEntity(entity);

			var transform = instance.GetComponent<Transform>();
			_hudRootEntity
				.Add<HudRoot>()
				.Add<Common.Component.Transform>().With(e => e.SetTransform(transform))
				;
			return entity;
		}

		public int CreateCharacterJoystick(Transform parent)
		{
			var characterJoystick = _kit.AssetProvider.CharacterJoystick();
			var instance =
				_kit.InstantiateService.Instantiate(characterJoystick, parent);
			var entity =
				_kit.EntityBehaviourFactory.InitEntityBehaviour(instance);
			return entity;
		}

		public int CreatePutBombButton(Transform parent)
		{
			var prefab = _kit.AssetProvider.PutBombButton();
			var instance = _kit.InstantiateService.Instantiate(prefab, parent);
			var entity = _kit.EntityBehaviourFactory.InitEntityBehaviour(instance);
			return entity;
		}
	}
}