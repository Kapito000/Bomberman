using Feature.Enemy.AI.Blackboard;
using UnityEngine;

namespace Feature.Enemy.Base.Component
{
	public struct EnemyParent { public Transform Value; }
	public struct EnemyComponent { }
	public struct EnemySpawnPoint { }
	public struct BaseEnemyAIBlackboardComponent { public BaseEnemyAIBlackboard Value; }
	public struct CurrentDestination { public Vector2 Value; }
}