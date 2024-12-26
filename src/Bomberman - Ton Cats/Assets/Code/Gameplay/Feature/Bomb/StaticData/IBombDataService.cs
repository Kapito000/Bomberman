using Gameplay.StaticData;

namespace Gameplay.Feature.Bomb.StaticData
{
	public interface IBombDataService : IStaticData
	{
		void Init();
		bool TryGet(BombType bombType, BombCharacteristic characteristic,
			out float value);
	}
}