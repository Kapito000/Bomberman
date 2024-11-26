using Gameplay.Feature.HUD.System;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature.HUD
{
	public sealed class HudFeature : Infrastructure.ECS.Feature
	{
		public HudFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddInit<CreateHudRootSystem>();
			AddInit<CreateCharacterMovementJoystickSystem>();
			AddInit<CreatePutBombButtonSystem>();
			AddInit<CreateUpperPanelSystem>();
			AddInit<CreateLifePointsSystem>();
			AddInit<CreateBombCounterPanelSystem>();
			
			AddLateUpdate<UpdateLifePointsSystem>();
			AddLateUpdate<UpdateBombCounterPanelSystem>();
		}
	}
}