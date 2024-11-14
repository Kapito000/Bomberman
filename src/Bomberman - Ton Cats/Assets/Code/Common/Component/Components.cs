using System;
using Infrastructure.ECS;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.U2D.Animation;

namespace Common.Component
{
	[Serializable] public struct Rigidbody2DComponent { public Rigidbody2D Value; }
	[Serializable] public struct TransformComponent { public Transform Value; }
	[Serializable] public struct SpriteLibraryComponent { public SpriteLibrary Value; }
	[Serializable] public struct NavMeshAgentComponent { public NavMeshAgent Value; }
	
	public struct View { public IEntityView Value; }
	
	[Serializable] public struct MoveSpeed { public float Value; }
	public struct MovementDirection { public Vector2 Value; }
	public struct Moving { }
	public struct Position { public Vector3 Value; }
	public struct Direction { public Vector2 Value; }
	public struct FirstBreath { }
	public struct ForParent { public Transform Value; }
}