using System;
using Extensions;
using Feature.Bomb;
using Feature.Bomb.Component;
using Feature.Input.Component;
using Infrastructure.ECS;
using Input.Character;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Feature.Input.System
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