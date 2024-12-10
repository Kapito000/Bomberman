using Gameplay.GameSettings.Audio;
using Infrastructure;

namespace Gameplay.GameSettings
{
	public interface IGameSettings : IService
	{
		IAudioSetting Audio { get; }
	}
}