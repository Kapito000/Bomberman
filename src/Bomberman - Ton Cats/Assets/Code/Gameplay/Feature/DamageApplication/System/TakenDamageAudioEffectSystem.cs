using Gameplay.Audio.Service;
using Gameplay.Feature.DamageApplication.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.DamageApplication.System
{
	public sealed class TakenDamageAudioEffectSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _effect;
		[Inject] IAudioService _audioService;

		readonly EcsFilterInject<Inc<Damage, TakenDamageAudioEffectId,
			TakenDamageEffectAudioSource>> _effectFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _effectFilter.Value)
			{
				_effect.SetEntity(e);
				var audioSource = _effect.TakenDamageEffectAudiosSource();
				var clipId = _effect.TakenDamageAudioEffectId();
				_audioService.Player.PlaySfx(clipId, audioSource);
			}
		}
	}
}