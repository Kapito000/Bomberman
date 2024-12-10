using Gameplay.GameSettings.StaticData.Audio;
using Gameplay.StaticData;

namespace Gameplay.GameSettings.StaticData
{
	public interface IGameSettingsStartValueData : IStaticData
	{
		IAudioStartValueData Audio { get; }
	}
}