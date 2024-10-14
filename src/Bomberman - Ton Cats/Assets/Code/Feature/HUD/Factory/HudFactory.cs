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
		[Inject] IFactoryKit _factoryKit;
		[Inject] EntityWrapper _hudRootEntity;

		public int CreateHudRoot(Transform parent)
		{
			var hudRoot = _factoryKit.AssetProvider.HudRoot();
			var instance = _factoryKit.InstantiateService.Instantiate(hudRoot, parent);
			var entity = _factoryKit.EntityBehaviourFactory
				.CreateEntityBehaviour(instance);
			_hudRootEntity.SetEntity(entity);

			var transform = instance.GetComponent<Transform>();
			_hudRootEntity
				.Add<HudRoot>()
				.Add<Common.Transform>().With(e => e.SetTransform(transform))
				;
			return entity;
		}

		public int CharacterJoystick(Transform parent)
		{
			var characterJoystick = _factoryKit.AssetProvider.CharacterJoystick();
			var instance =
				_factoryKit.InstantiateService.Instantiate(characterJoystick, parent);
			var entity =
				_factoryKit.EntityBehaviourFactory.CreateEntityBehaviour(instance);
			return entity;
		}
	}
}