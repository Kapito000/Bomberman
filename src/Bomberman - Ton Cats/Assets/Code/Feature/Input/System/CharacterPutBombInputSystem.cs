using System;
using Common;
using Extensions;
using Infrastructure.ECS;
using Input.Character;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Feature.Input.System
{
	public sealed class CharacterPutBombInputSystem : EcsSystem, IEcsInitSystem,
		IDisposable
	{
		[Inject] ICharacterInput _characterInput;

		readonly EcsFilterInject<
			Inc<InputReader, CharacterInput, BombCarrier>,
			Exc<PutBombRequest>> _filter;

		public void Init(IEcsSystems systems)
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
				_world.RequestPutBomb(e);
			}
		}
	}
}