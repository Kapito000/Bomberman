using UnityEngine;
using UnityEngine.Audio;
using Menu = Constant.CreateAssetMenu;

namespace Gameplay.Audio.MixerGroupProvider
{
	[CreateAssetMenu(menuName = Menu.Path.c_StaticData + nameof(AudioMixerProvider))]
	public sealed class AudioMixerProvider : ScriptableObject,
		IAudioMixerProvider
	{
		[field: SerializeField] public AudioMixer Mixer { get; private set; }
		[SerializeField] MixerGroupDictionary _groups;

		public bool TryGetMixerGroup(MixerGroup groupType, out AudioMixerGroup group) =>
			_groups.TryGetValue(groupType, out group);
	}
}