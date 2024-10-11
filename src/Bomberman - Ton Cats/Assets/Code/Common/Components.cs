using System;
using Infrastructure.ECS;
using UnityEngine;

namespace Common.Component
{
	[Serializable]
	public struct Transform { public UnityEngine.Transform Value; }
	
	public struct View { public IEntityView Value; }
	
	public struct PutBombRequest { }
	public struct BombCarrier { }
	public struct MovableDirection { public Vector2 Value; }
}