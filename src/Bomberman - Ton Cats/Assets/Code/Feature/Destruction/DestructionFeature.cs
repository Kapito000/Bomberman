using Factory.SystemFactory;
using Feature.Destruction.System;

namespace Feature.Destruction
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