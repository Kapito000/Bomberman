using System.IO;
using UnityEngine;
using Zenject;

namespace Gameplay.SaveLoad
{
	public sealed class SaveLoadService : ISaveLoadService
	{
		const string c_saveFileName = "Bomberman save.json";

		[Inject] ISaveProcessor[] _saveProcessors;

		public SavedData SavedData { get; } = new();

		public void Save()
		{
			foreach (var processor in _saveProcessors)
				processor.Save();

			var json = JsonUtility.ToJson(SavedData);
			var path = Path.Combine(Application.persistentDataPath, c_saveFileName);
			File.WriteAllText(path, json);
		}
	}
}