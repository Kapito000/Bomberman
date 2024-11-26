using System;

namespace Gameplay.Feature.Life.Component
{
	[Serializable] public struct LifePoints { public int Value; }
	public struct ChangeLifePoints { public int Value; }
	public struct Dead { }
	public struct DeathProcessor { }
}