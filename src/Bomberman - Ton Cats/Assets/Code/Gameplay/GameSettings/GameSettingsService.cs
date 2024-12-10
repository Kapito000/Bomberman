using Gameplay.GameSettings.Audio;
using Zenject;

namespace Gameplay.GameSettings
{
	public sealed class GameSettingsService : IGameSettings
	{
		[Inject] public IAudioSetting Audio { get; }
	}
}