using System;
using System.Collections.Generic;
using Common.Collections;
using Gameplay.Feature.Bomb.Behaviour;
using Leopotam.EcsLite;
using UnityEngine;

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
	public struct TargetsBufferIncrementRequest { public List<int> Value; }
	public struct TargetsBufferDecrementRequest { public List<int> Value; }
	public struct TargetsBuffer { public List<EcsPackedEntityWithWorld> Value; }
	public struct ExplodedTargetsBuffer { public List<EcsPackedEntityWithWorld> Value; }
	[Serializable] public struct BombAnimatorComponent { public BombAnimator Value; }
}