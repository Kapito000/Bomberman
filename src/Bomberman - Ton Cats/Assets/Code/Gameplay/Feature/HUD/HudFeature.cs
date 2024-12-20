using Gameplay.Feature.Enemy.System;
using Gameplay.Feature.HUD.Feature.Bomb.System;
using Gameplay.Feature.HUD.Feature.Enemy.System;
using Gameplay.Feature.HUD.Feature.Input.System;
using Gameplay.Feature.HUD.Feature.Life.System;
using Gameplay.Feature.HUD.Feature.Timer.System;
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
			AddInit<CreateGameTimerDisplaySystem>();
			AddInit<CreateEnemyCounterDisplaySystem>();
			
			AddLateUpdate<UpdateLifePointsSystem>();
			AddLateUpdate<UpdateBombCounterPanelSystem>();
			AddLateUpdate<UpdateGameTimerDisplaySystem>();
			AddLateUpdate<UpdateEnemyCounterDisplaySystem>();
		}
	}
}