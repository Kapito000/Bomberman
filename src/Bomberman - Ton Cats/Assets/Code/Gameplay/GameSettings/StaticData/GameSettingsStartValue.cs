using Gameplay.GameSettings.StaticData.Audio;

namespace Gameplay.GameSettings.StaticData
{
	public sealed class GameSettingsStartValue : IGameSettingsStartValueData
	{
		public IAudioStartValueData Audio { get; } = new AudioStartValueData();
	}
}