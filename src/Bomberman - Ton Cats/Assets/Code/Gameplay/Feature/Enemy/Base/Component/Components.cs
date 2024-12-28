using Gameplay.Feature.Enemy.AI.Blackboard;
using UnityEngine;

namespace Gameplay.Feature.Enemy.Base.Component
{
	public struct EnemyId {		public string Value; }
	public struct Walking { }
	public struct Volatile { }
	public struct PatrolPoint { public Vector2 Value; }
	public struct EnemyParent { public Transform Value; }
	public struct EnemyComponent { }
	public struct EnemySpawnPoint { }
	public struct EnemySpawnRequest { }
	public struct EnemyAIBlackboardComponent { public EnemyAIBlackboard Value; }
}