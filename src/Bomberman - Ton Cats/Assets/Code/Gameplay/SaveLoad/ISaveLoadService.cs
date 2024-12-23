using Infrastructure;

namespace Gameplay.SaveLoad
{
	public interface ISaveLoadService : IService
	{
		void Save();
		void Load();
	}
}