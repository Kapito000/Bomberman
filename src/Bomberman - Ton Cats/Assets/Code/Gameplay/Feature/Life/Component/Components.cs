using System;

namespace Gameplay.Feature.Life.Component
{
	[Serializable] public struct LifePoints { public int Value; }
	public struct Dead { }
	public struct Immortal { }
	public struct ImmortalTimer { public float Value; }
	public struct DeathProcessor { }
	public struct ChangeLifePoints { public int Value; }
	public struct DeathAudioEffectClipId { public string Value; }
}