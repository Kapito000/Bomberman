using System.Collections.Generic;
using Feature.Enemy.AI.Blackboard;
using UnityEngine;

namespace Feature.Enemy.Base.Component
{
	public struct EnemyComponent { }
	public struct EnemySpawnPoint { }
	public struct BaseEnemyAIBlackboardComponent { public BaseEnemyAIBlackboard Value; }
	public struct PatrolPoints { public List<Transform> Value; }
	public struct CurrentPatrolPoint { public Transform Value; }
}