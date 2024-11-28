using Gameplay.Feature.HUD.Feature.Bomb.Behaviour;
using Gameplay.Feature.HUD.Feature.Life.Behaviour;
using Gameplay.Feature.HUD.Feature.Timer.Behaviour;

namespace Gameplay.Feature.HUD.Component
{
	public struct HudRoot { }
	public struct UpperPanel { }
	public struct LifePointsPanelComponent { public LifePointsPanel Value; }
	public struct BombCounterPanelComponent { public BombCounterPanel Value; }
	public struct GameTimerDisplayComponent { public GameTimerDisplay Value; }
}