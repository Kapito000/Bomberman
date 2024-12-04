using UnityEngine;
using Menu = Constant.CreateAssetMenu;

namespace Gameplay.Audio.ClipProvider
{
	[CreateAssetMenu(menuName = Menu.Path.c_StaticData + nameof(AudioClipProvider))]
	public sealed class AudioClipProvider : ScriptableObject, IAudioClipProvider
	{
		[field: SerializeField] public MusicLibrary Musics { get; private set; }
	}
}