using System;
using Common.Collections;
using Gameplay.Feature.Bomb.Behaviour;

namespace Gameplay.Feature.Bomb.Component
{
	public struct BombStack { public StackList<BombType> Value; }
	public struct BombParent { }
	public struct BombCarrier { }
	public struct BombComponent { }
	public struct BombExplosion { }
	public struct ExplosionTimer { public float Value; }
	public struct PutBombRequest { }
	public struct ExplosionRadius { public int Value; }
	
	public struct ExplosionPartComponent { public ExplosionPart Value; }
	
	public struct CallExplosion { }
	
	public struct Explosion { }
	public struct CreateExplosionRequest { }
	public struct BlowUpDestructible { }
	[Serializable] public struct BombAnimatorComponent { public BombAnimator Value; }
}