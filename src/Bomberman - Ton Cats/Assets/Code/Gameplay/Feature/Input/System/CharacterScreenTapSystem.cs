using System;
using Extensions;
using Gameplay.Feature.Bomb.Component;
using Gameplay.Feature.Input.Component;
using Gameplay.Input.Character;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;
using Vector2 = UnityEngine.Vector2;

namespace Gameplay.Feature.Input.System
{
	public sealed class CharacterScreenTapSystem : IEcsRunSystem,IDisposable
	{
		[Inject] EntityWrapper _entity;
		[Inject] ICharacterInput _characterInput;

		readonly EcsFilterInject<
			Inc<InputReader, CharacterInput, BombCarrier>,
			Exc<ScreenTap>> _filter;

		public void Run(IEcsSystems systems)
		{
			_characterInput.ScreenClick += OnScreenTap;
		}

		public void Dispose()
		{
			_characterInput.ScreenClick -= OnScreenTap;
		}

		void OnScreenTap(Vector2 screenPos)
		{
			foreach (var e in _filter.Value)
			{
				_entity.SetEntity(e);
				_entity.AddScreenTap(screenPos);
			}
		}
	}
}