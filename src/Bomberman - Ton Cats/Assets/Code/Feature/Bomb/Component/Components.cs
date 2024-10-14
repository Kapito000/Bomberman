using System;
using Feature.Bomb.Behaviour;

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
}