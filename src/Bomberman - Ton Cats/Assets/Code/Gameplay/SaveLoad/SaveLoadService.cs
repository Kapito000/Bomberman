using System.IO;
using UnityEngine;
using Zenject;

namespace Gameplay.SaveLoad
{
	public sealed class SaveLoadService : ISaveLoadService
	{
		const string c_saveFileName = "Bomberman save.json";

		[Inject] ISaveProcessor[] _saveProcessors;
		[Inject] ILoadProcessor[] _loadProcessors;

		readonly SavedData _savedData = new();

		public void Save()
		{
			foreach (var processor in _saveProcessors)
				processor.Save(_savedData);

			var json = JsonUtility.ToJson(_savedData, true);
			var path = SaveFilePath();
			File.WriteAllText(path, json);
		}

		public void Load()
		{
			var saveFilePath = SaveFilePath();
			if (File.Exists(saveFilePath) == false)
				return;

			var json = File.ReadAllText(saveFilePath);
			JsonUtility.FromJsonOverwrite(json, _savedData);

			foreach (var processor in _loadProcessors)
				processor.Load(_savedData);
		}

		public string SaveFilePath() =>
			Path.Combine(Application.persistentDataPath, c_saveFileName);
	}
}