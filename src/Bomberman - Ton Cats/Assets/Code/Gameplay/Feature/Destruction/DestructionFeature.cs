using Gameplay.Feature.Destruction.System;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature.Destruction
{
	public sealed class DestructionFeature : Infrastructure.ECS.Feature
	{
		public DestructionFeature(ISystemFactory systemFactory) : base(
			systemFactory)
		{
#if UNITY_EDITOR
			// AddInit<ShowViewNamesInConsoleSystem>();
#endif
			AddCleanup<CommonCleanupSystem>();
			AddCleanup<DestructionViewSystem>();
			AddCleanup<DestructionSystem>();
		}
	}
}