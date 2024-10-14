using Factory.SystemFactory;
using Feature.HUD.System;

namespace Feature.HUD
{
	public sealed class HudFeature : Infrastructure.ECS.Feature
	{
		public HudFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddInit<CreateHudRootSystem>();
			AddInit<CreateCharacterMovementJoystickSystem>();
			AddInit<CreatePutBombButtonSystem>();
		}
	}
}