using Gameplay.Feature.UI.Component;
using Infrastructure.ECS;
using Infrastructure.Factory.Kit;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.UI.Factory
{
	public sealed class UiFactory : IUiFactory
	{
		[Inject] IFactoryKit _factoryKit;
		[Inject] EntityWrapper _entity;

		public int CreateRootCanvas()
		{
			var prefab = _factoryKit.AssetProvider.UiRoot();
			var instance = _factoryKit.InstantiateService.Instantiate(prefab);
			var entity = _factoryKit.EntityBehaviourFactory
				.InitEntityBehaviour(instance);
			_entity.SetEntity(entity);

			var transform = instance.GetComponent<Transform>();
			_entity
				.Add<UiRoot>()
				.AddTransform(transform);
			;

			return entity;
		}

		public void EventSystem()
		{
			var prefab = _factoryKit.AssetProvider.EventSystem();
			_factoryKit.InstantiateService.Instantiate(prefab);
		}
	}
}