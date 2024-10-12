using Factory.EntityBehaviourFactory;
using Infrastructure.AssetProvider;
using InstantiateService;
using Zenject;

namespace Factory.Kit
{
	public sealed class FactoryKit : IFactoryKit
	{
		[Inject] public IAssetProvider AssetProvider { get; }
		[Inject] public IInstantiateService InstantiateService { get; }
		[Inject] public IEntityBehaviourFactory EntityBehaviourFactory { get; }
	}
}