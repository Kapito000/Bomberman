using Common.Component;
using Gameplay.Audio.ClipProvider;
using Gameplay.Audio.MixerGroupProvider;
using Gameplay.Audio.Player;
using Gameplay.LevelData;
using Infrastructure.ECS;
using UnityEngine;
using Zenject;

namespace Gameplay.Audio.Service
{
	public sealed class AudioService : IAudioService
	{
		[Inject] ILevelData _levelData;
		[Inject] IAudioClipProvider _clipProvider;
		[Inject] IAudioMixerProvider _mixerProvider;

		[Inject] public IAudioPlayer Player { get; }

		public void AssignMusicClip(AmbientMusic key, AudioSource audioSource)
		{
			if (_clipProvider.TryGetWithDebug(key, out var clip))
				audioSource.clip = clip;
		}

		public void AssignUiSfxClip(UiSfx key, AudioSource audioSource)
		{
			if (_clipProvider.TryGetWithDebug(key, out var clip))
				audioSource.clip = clip;
		}
		
		public void AssignMixerGroup(MixerGroup groupType, AudioSource audioSource)
		{
			if (_mixerProvider.TryGetMixerGroup(groupType, out var group))
				audioSource.outputAudioMixerGroup = group;
			else
				Debug.LogError($"Cannot to get \"{groupType}\" group.");
		}

		public void EstablishCommonSettings(AudioSource audioSource)
		{
			audioSource.playOnAwake = false;
		}

		public bool TryCreateAdditionalAudioSource(int forEntity,
			out AudioSource audioSource, string name)
		{
			var wrapper = _levelData.NewEntityWrapper();
			wrapper.SetEntity(forEntity);
			if (TryGetTransform(wrapper, out var tr) == false)
			{
				audioSource = default;
				return false;
			}

			ReplaceAudioSourceParent(wrapper, tr);
			var audioSourceObj = CreateAudioSourceObj(name, wrapper);
			audioSource = ReplaceAudioSource(audioSourceObj);
			return true;
		}

		public AudioSource ReplaceAudioSource(GameObject instance)
		{
			var audioSource = instance.GetComponent<AudioSource>();
			if (audioSource == null)
				audioSource = instance.AddComponent<AudioSource>();
			return audioSource;
		}

		static GameObject CreateAudioSourceObj(string name, EntityWrapper wrapper)
		{
			var parent = wrapper.AdditionalAudioSourceParent();
			var audioSourceObj = new GameObject(name);
			audioSourceObj.transform.SetParent(parent);
			return audioSourceObj;
		}

		void ReplaceAudioSourceParent(EntityWrapper wrapper, Transform tr)
		{
			if (wrapper.Has<AdditionalAudioSourceParent>() == false)
			{
				var name = Constant.ObjectName.c_AdditionalAudioSourceParent;
				var parent = new GameObject(name);
				parent.transform.SetParent(tr, false);
				wrapper.AddAdditionalAudioSourceParent(parent.transform);
			}
		}

		bool TryGetTransform(EntityWrapper wrapper, out Transform transform)
		{
			if (wrapper.Has<TransformComponent>() == false)
			{
				transform = default;
				return false;
			}
			transform = wrapper.Transform();
			return true;
		}
	}
}