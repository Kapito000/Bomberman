using Gameplay.LevelData;
#if UNITY_EDITOR
using Mitfart.LeoECSLite.UnityIntegration.View;
#endif
using UnityEngine;
using Zenject;

namespace Infrastructure.ECS
{
	public partial class EntityBehaviour
	{
#if UNITY_EDITOR
		[Inject] ILevelData _levelData;
		[SerializeField] EntityView _entityView;
#endif

		void InitPartial(int entity)
		{
#if UNITY_EDITOR
			_entityView = _levelData.EcsWorldDebugSystem.View.GetEntityView(entity);
#endif
		}
	}
}