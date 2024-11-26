using System;
using Extensions;
using Gameplay.Feature.Bomb.Component;
using Gameplay.Feature.Input.Component;
using Gameplay.Input.Character;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Input.System
{
	public sealed class CharacterPutBombInputSystem : IEcsRunSystem,
		IDisposable
	{
		[Inject] EcsWorld _world;
		[Inject] ICharacterInput _characterInput;

		readonly EcsFilterInject<
			Inc<InputReader, CharacterInput, BombCarrier>,
			Exc<PutBombRequest>> _filter;

		public void Run(IEcsSystems systems)
		{
			_characterInput.PutBomb += OnPutBombInput;
		}

		public void Dispose()
		{
			_characterInput.PutBomb -= OnPutBombInput;
		}

		void OnPutBombInput()
		{
			foreach (var e in _filter.Value)
			{
				_world.AddComponent<PutBombRequest>(e);
			}
		}
	}
}