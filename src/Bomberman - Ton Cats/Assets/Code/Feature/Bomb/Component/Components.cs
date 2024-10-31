using System;
using Feature.Bomb.Behaviour;
using MapTile;

namespace Feature.Bomb.Component
{
	public struct Bomb { }
	public struct BombNumber	{	public int Value; }
	public struct BombParent { }
	public struct BombCarrier { }
	public struct PutBombRequest { }
	[Serializable]
	public struct BombAnimatorComponent { public BombAnimator Value; }
	public struct Explosion { }
	public struct ExplosionRequest { }
	public struct CreateExplosionRequest { }
	public struct BlowUpDestructible { public IDestructible Destructible; }
	public struct ExplosionPart { public Feature.Bomb.EExplosionPart Value; }
	public struct ExplosionCenter { }
}