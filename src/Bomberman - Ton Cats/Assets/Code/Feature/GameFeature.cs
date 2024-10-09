using Infrastructure.SystemFactory;
using Leopotam.EcsLite;

namespace Feature
{
	public sealed class GameFeature : Infrastructure.Feature
	{
		public GameFeature(EcsSystems systems, ISystemFactory systemFactory)
			: base(systems, systemFactory)
		{
			Add<global::Feature.Hero.Feature>();
		}
	}
}