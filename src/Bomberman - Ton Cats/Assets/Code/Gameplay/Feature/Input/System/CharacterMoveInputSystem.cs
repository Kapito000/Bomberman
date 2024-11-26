using Common.Component;
using Extensions;
using Gameplay.Feature.Input.Component;
using Gameplay.Input.Character;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Input.System
{
	public sealed class CharacterMoveInputSystem : IEcsRunSystem
	{
		[Inject] EcsWorld _world;
		[Inject] ICharacterInput _characterInput;

		readonly EcsFilterInject<
			Inc<InputReader, CharacterInput, MovementDirection>> _filter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _filter.Value)
			{
				_world.SetMovementDirection(e, _characterInput.Movement);
			}
		}
	}
}