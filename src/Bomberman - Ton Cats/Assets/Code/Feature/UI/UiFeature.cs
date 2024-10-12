using Factory.SystemFactory;
using Feature.UI.System;

namespace Feature.UI
{
	public sealed class UiFeature : Infrastructure.ECS.Feature
	{
		public UiFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddInit<CreateRootCanvasSystem>();
		}
	}
}