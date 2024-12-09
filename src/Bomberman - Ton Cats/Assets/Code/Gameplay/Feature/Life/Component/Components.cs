using System;
using Infrastructure.ECS;
using UnityEngine;

namespace Gameplay.Feature.Life.Component
{
	[Serializable] public struct LifePoints { public int Value; }
	public struct ChangeLifePoints { public int Value; }
	public struct Dead { }
	public struct DeathProcessor { }
	public struct DeathAudioEffectClipId { public string Value; }
}