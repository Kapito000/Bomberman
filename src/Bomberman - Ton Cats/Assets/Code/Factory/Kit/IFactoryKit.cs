using Factory.EntityBehaviourFactory;
using Infrastructure.AssetProvider;
using InstantiateService;

namespace Factory.Kit
{
	public interface IFactoryKit
	{
		IAssetProvider AssetProvider { get; }
		IInstantiateService InstantiateService { get; }
		IEntityBehaviourFactory EntityBehaviourFactory { get; }
	}
}