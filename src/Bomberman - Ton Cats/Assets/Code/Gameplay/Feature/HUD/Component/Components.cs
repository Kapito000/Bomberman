using Common.HUD;
using Gameplay.Feature.HUD.Feature.Timer.Behaviour;

namespace Gameplay.Feature.HUD.Component
{
	public struct HudRoot { }
	public struct UpperPanel { }
	public struct LifePointsPanel { public IDisplay<int> Value; }
	public struct BombCounterPanel { public IDisplay<int> Value; }
	public struct EnemyCounterDisplay { public IDisplay<int> Value; }
	public struct GameTimerDisplayComponent { public GameTimerDisplay Value; }
}