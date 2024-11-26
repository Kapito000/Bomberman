using System;
using Gameplay.Feature.Bomb.Behaviour;

namespace Gameplay.Feature.Bomb.Component
{
	public struct Bomb { }
	public struct BombNumber	{	public int Value; }
	public struct BombParent { }
	public struct BombCarrier { }
	public struct PutBombRequest { }
	[Serializable]
	public struct BombAnimatorComponent { public BombAnimator Value; }
}