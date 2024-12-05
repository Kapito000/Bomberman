using Gameplay.Audio.Library;
using Infrastructure;

namespace Gameplay.Audio.ClipProvider
{
	public interface IAudioClipProvider : IService
	{
		MusicLibrary Musics { get; }
		ShortMusicLibrary ShortMusic { get; }
	}
}