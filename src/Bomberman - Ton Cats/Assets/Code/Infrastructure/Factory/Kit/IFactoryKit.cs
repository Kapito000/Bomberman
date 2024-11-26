using Infrastructure.AssetProvider;
using Infrastructure.Factory.EntityBehaviourFactory;
using Infrastructure.InstantiateService;

namespace Infrastructure.Factory.Kit
{
	public interface IFactoryKit
	{
		IAssetProvider AssetProvider { get; }
		IInstantiateService InstantiateService { get; }
		IEntityBehaviourFactory EntityBehaviourFactory { get; }
	}
}