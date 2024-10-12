using Extensions;
using Factory.Kit;
using Feature.UI;
using Infrastructure.ECS;
using UnityEngine;
using Zenject;

namespace Factory.UiFactory
{
	public sealed class UiFactory : IUiFactory
	{
		[Inject] IFactoryKit _factoryKit;
		[Inject] EntityWrapper _rootEntity;
		[Inject] EntityWrapper _uiRootEntity;

		public int CreateRootCanvas()
		{
			var prefab = _factoryKit.AssetProvider.UiRoot();
			var rootInstance = _factoryKit.InstantiateService.Instantiate(prefab);
			var entity = _factoryKit.EntityBehaviourFactory
				.CreateEntityBehaviour(rootInstance);

			var transform = rootInstance.GetComponent<Transform>();
			_uiRootEntity
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