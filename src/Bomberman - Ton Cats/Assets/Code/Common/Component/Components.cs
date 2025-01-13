using System;
using Infrastructure.ECS;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.U2D.Animation;

namespace Common.Component
{
	[Serializable] public struct CameraComponent { public Camera Value; }
	[Serializable] public struct CanvasComponent { public Canvas Value; }
	[Serializable] public struct TransformComponent { public Transform Value; }
	[Serializable] public struct AudioSourceComponent { public AudioSource Value; }
	[Serializable] public struct Rigidbody2DComponent { public Rigidbody2D Value; }
	[Serializable] public struct NavMeshAgentComponent { public NavMeshAgent Value; }
	[Serializable] public struct SpriteLibraryComponent { public SpriteLibrary Value; }
	[Serializable] public struct SpriteRendererComponent { public SpriteRenderer Value; }
	
	public struct View { public IEntityView Value; }
	
	[Serializable] public struct MoveSpeed { public float Value; }
	public struct MovementDirection { public Vector2 Value; }
	public struct Moving { }
	public struct Position { public Vector3 Value; }
	public struct Direction { public Vector2 Value; }
	public struct FirstBreath { }
	public struct Parent { public Transform Value; }
	public struct Launched { }
	public struct AdditionalAudioSourceParent { public Transform Value; }
}