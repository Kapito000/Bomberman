using Factory.SystemFactory;
using Feature.Destruction.System;

namespace Feature.Destruction
{
	public sealed class DestructionFeature : Infrastructure.ECS.Feature
	{
		public DestructionFeature(ISystemFactory systemFactory) : base(
			systemFactory)
		{
			AddCleanup<DestructionViewSystem>();
			AddCleanup<DestructionSystem>();
		}
	}
}