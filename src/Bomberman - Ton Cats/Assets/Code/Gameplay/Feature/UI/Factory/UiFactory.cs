using Gameplay.Feature.UI.Component;
using Infrastructure.ECS;
using Infrastructure.Factory.Kit;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.UI.Factory
{
	public sealed class UiFactory : IUiFactory
	{
		[Inject] IFactoryKit _kit;
		[Inject] EntityWrapper _entity;

		public int CreateRootCanvas()
		{
			var prefab = _kit.AssetProvider.UiRoot();
			var instance = _kit.InstantiateService.Instantiate(prefab);
			var e = _kit.EntityBehaviourFactory.InitEntityBehaviour(instance);
			_entity.SetEntity(e);

			_entity
				.Add<UiRoot>()
				.AddTransform(instance.transform);
			;
			return e;
		}

		public int WindowsRoot(Transform parent)
		{
			var prefab = _kit.AssetProvider.WindowsRoot();
			var instance = _kit.InstantiateService.Instantiate(prefab, parent);
			var e = _kit.EntityBehaviourFactory.InitEntityBehaviour(instance);

			_entity.SetEntity(e);
			_entity
				.Add<WindowsRoot>()
				.AddTransform(instance.transform);
			;
			return e;
		}

		public void EventSystem()
		{
			var prefab = _kit.AssetProvider.EventSystem();
			_kit.InstantiateService.Instantiate(prefab);
		}
	}
}