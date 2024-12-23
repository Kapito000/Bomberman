using Gameplay.SaveLoad;

namespace Gameplay.Progress
{
	public sealed class ProgressService : IProgressService, ISaveProcessor,
		ILoadProcessor
	{
		public int ReachedLevel { get; set; }

		public void Save(SavedData savedData)
		{
			savedData.ReachedLevel = ReachedLevel;
		}

		public void Load(SavedData savedData)
		{
			ReachedLevel = savedData.ReachedLevel;
		}
	}
}