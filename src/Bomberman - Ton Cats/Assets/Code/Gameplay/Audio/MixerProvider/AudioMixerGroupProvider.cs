using UnityEngine;
using UnityEngine.Audio;
using Menu = Constant.CreateAssetMenu;

namespace Gameplay.Audio.MixerProvider
{
	[CreateAssetMenu(menuName = Menu.Path.c_StaticData + nameof(AudioMixerGroupProvider))]
	public sealed class AudioMixerGroupProvider : ScriptableObject,
		IAudioMixerGroupProvider
	{
		[field: SerializeField] public AudioMixer Mixer { get; private set; }
		[SerializeField] MixerGroupDictionary _groups;

		public AudioMixerGroup MixerGroup(MixerGroup group)
		{
			if (_groups.TryGetValue(group, out var mixerGroup) == false)
			{
				Debug.LogError($"Cannot to find mixer group: \"{group}\". " +
					$"Returned the main group.");
				return _groups[Audio.MixerGroup.Main];
			}

			return mixerGroup;
		}

		public bool Has(MixerGroup group)
		{
			if (_groups.TryGetValue(group, out var mixerGroup) &&
			    mixerGroup != null)
			{
				return true;
			}
			return false;
		}
	}
}