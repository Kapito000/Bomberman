using System;
using Common.Collections;
using Gameplay.Feature.Bomb.Behaviour;

namespace Gameplay.Feature.Bomb.Component
{
	public struct BombStack { public StackList<BombType> Value; }
	public struct BombNumber { public int Value; }
	public struct BombParent { }
	public struct BombCarrier { }
	public struct BombComponent { }
	public struct PutBombRequest { }
	[Serializable]
	public struct BombAnimatorComponent { public BombAnimator Value; }
}