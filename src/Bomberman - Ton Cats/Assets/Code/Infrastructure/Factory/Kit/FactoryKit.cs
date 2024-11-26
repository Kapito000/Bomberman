using Infrastructure.AssetProvider;
using Infrastructure.Factory.EntityBehaviourFactory;
using Infrastructure.InstantiateService;
using Zenject;

namespace Infrastructure.Factory.Kit
{
	public sealed class FactoryKit : IFactoryKit
	{
		[Inject] public IAssetProvider AssetProvider { get; }
		[Inject] public IInstantiateService InstantiateService { get; }
		[Inject] public IEntityBehaviourFactory EntityBehaviourFactory { get; }
	}
}