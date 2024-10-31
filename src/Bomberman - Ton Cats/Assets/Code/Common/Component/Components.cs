﻿using System;
using Infrastructure.ECS;
using UnityEngine;

namespace Common.Component
{
	[Serializable] public struct Transform { public UnityEngine.Transform Value; }
	[Serializable] public struct Rigidbody2D { public UnityEngine.Rigidbody2D Value; }
	
	public struct View { public IEntityView Value; }
	
	[Serializable] public struct MoveSpeed { public float Value; }
	public struct MovementDirection { public Vector2 Value; }
	public struct Position { public Vector3 Value; }
	public struct Direction { public Vector2 Value; }
	public struct LifePoints { public int Value; }
	public struct FirstBreath { }
	public struct ForParent { public UnityEngine.Transform Value; }
}