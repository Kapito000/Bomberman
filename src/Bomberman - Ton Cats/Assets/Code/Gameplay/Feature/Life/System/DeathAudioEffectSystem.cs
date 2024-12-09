using Common.Component;
using Gameplay.Audio.Service;
using Gameplay.Feature.Life.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Life.System
{
	public sealed class DeathAudioEffectSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _entity;
		[Inject] IAudioService _audioService;

		readonly EcsFilterInject<
				Inc<DeathProcessor, TransformComponent, DeathAudioEffectClipId>>
			_effectFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _effectFilter.Value)
			{
				_entity.SetEntity(e);
				var clipId = _entity.DeathAudioEffectClipId();
				var pos = _entity.TransformPos();
				_audioService.Player.PlaySfxClipAtPoint(clipId, pos);
			}
		}
	}
}