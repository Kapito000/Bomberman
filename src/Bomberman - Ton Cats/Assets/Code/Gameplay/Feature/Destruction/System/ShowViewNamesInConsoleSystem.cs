using Common.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.Destruction.System
{
#if UNITY_EDITOR
	public sealed class ShowViewNamesInConsoleSystem : IEcsInitSystem
	{
		[Inject] EntityWrapper _entity;
		readonly EcsFilterInject<Inc<View>> _viewFilter;

		public void Init(IEcsSystems systems)
		{
			foreach (var e in _viewFilter.Value)
			{
				_entity.SetEntity(e);
				Debug.Log(_entity.View().gameObject.name);
			}
		}
	}
#endif
}