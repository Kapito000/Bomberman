using Common.Component;
using Extensions;
using Feature.Input.Component;
using Infrastructure.ECS;
using Input.Character;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Feature.Input.System
{
	public sealed class MoveInputSystem : EcsSystem, IEcsRunSystem
	{
		[Inject] ICharacterInput _characterInput;

		readonly EcsFilterInject<
			Inc<InputReader, CharacterInput, MovableDirection>> _filter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _filter.Value)
			{
				_world.SetMovableDirection(e, _characterInput.Movement);
			}
		}
	}
}