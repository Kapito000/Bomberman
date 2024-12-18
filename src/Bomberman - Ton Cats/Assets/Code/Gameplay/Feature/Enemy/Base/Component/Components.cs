using Gameplay.Feature.Enemy.AI.Blackboard;
using UnityEngine;

namespace Gameplay.Feature.Enemy.Base.Component
{
	public struct EnemyId {		public string Value; }
	public struct Walking { }
	public struct Volatile { }
	public struct EnemyParent { public Transform Value; }
	public struct EnemyComponent { }
	public struct EnemySpawnPoint { }
	public struct EnemyAIBlackboardComponent { public EnemyAIBlackboard Value; }
	public struct CurrentDestination { public Vector2 Value; }
}