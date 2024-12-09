using UnityEngine;

namespace Gameplay.Audio.ClipProvider
{
	public static class AudioClipProviderExtension
	{
		public static bool TryGetWithDebug(this IAudioClipProvider provider,
			AmbientMusic key, out AudioClip clip)
		{
			if (provider.Musics.TryGetValue(key, out clip))
				return true;

			Debug.LogError($"Cannot to get \"{key}\" clip.");
			return false;
		}

		public static bool TryGetWithDebug(this IAudioClipProvider provider,
			ShortMusic key, out AudioClip clip)
		{
			if (provider.ShortMusic.TryGetValue(key, out clip))
				return true;

			Debug.LogError($"Cannot to get \"{key}\" clip.");
			return false;
		}
		
		public static bool TryGetWithDebug(this IAudioClipProvider provider,
			UiSfx key, out AudioClip clip)
		{
			if (provider.UiSfx.TryGetValue(key, out clip))
				return true;

			Debug.LogError($"Cannot to get \"{key}\" clip.");
			return false;
		}
		
		public static bool TryGetSfxWithDebug(this IAudioClipProvider provider,
			string key, out AudioClip clip)
		{
			if (provider.Sfx.TryGetValue(key, out clip))
				return true;

			Debug.LogError($"Cannot to get \"{key}\" clip.");
			return false;
		}
	}
}