using Infrastructure;

namespace Gameplay.Audio.ClipProvider
{
	public interface IAudioClipProvider : IService
	{
		MusicLibrary Musics { get; }
	}
}