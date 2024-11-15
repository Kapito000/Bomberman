using System.Collections.Generic;
using Leopotam.EcsLite;

namespace Feature.DamageApplication.Component
{
	public struct Damage { public int Value; }
	public struct DamageBuffer { public List<EcsPackedEntityWithWorld> Value; }
	public struct DamageBufferIncrementRequest { public List<int> Value; }
	public struct DamageBufferDecrementRequest { public List<int> Value; }
	public struct TakenDamageEffect { }
	public struct TakenDamageEffectDuration { public float Value; }
	public struct SpriteFlickeringPeriod { public float Value; }
	public struct SpriteFlickeringTimer { public float Value; }
}