using Factory.EntityBehaviourFactory;
using Infrastructure.AssetProvider;
using InstantiateService;

namespace Factory
{
	public interface IFactoryKit
	{
		IAssetProvider AssetProvider { get; }
		IInstantiateService InstantiateService { get; }
		IEntityBehaviourFactory EntityBehaviourFactory { get; }
	}
}