using Infrastructure.ECS;
using Zenject;

namespace Factory.UiFactory
{
	public sealed class UiFactory : IUiFactory
	{
		[Inject] IFactoryKit _factoryKit;
		[Inject] EntityWrapper _rootEntity;

		public int CreateRootCanvas()
		{
			var prefab = _factoryKit.AssetProvider.UiRoot();
			var rootInstance = _factoryKit.InstantiateService.Instantiate(prefab);
			var entity = _factoryKit.EntityBehaviourFactory
				.CreateEntityBehaviour(rootInstance);

			return entity;
		}
	}
}