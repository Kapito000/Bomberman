using Gameplay.SaveLoad;
using Zenject;

namespace Gameplay.Progress
{
	public sealed class ProgressService : IProgressService, ISaveProcessor
	{
		[Inject] ISaveLoadService _saveLoadService;
		
		public int ReachedLevel { get; set; }
		
		public void Save()
		{
			_saveLoadService.SavedData.ReachedLevel = ReachedLevel;
		}
	}
}