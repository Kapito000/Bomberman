using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Infrastructure.SceneLoader
{
	public class StandardLoader : ISceneLoader
	{
		public async UniTask LoadAsync(string sceneName)
		{
			await SceneManager.LoadSceneAsync(sceneName).ToUniTask();
		}
		
		public async UniTask LoadAsync(string sceneName, Action onLoaded)
		{
			await SceneManager.LoadSceneAsync(sceneName).ToUniTask();
			onLoaded?.Invoke();
		}
	}
}