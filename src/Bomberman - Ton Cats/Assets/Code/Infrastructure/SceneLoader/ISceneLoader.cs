using System;
using Cysharp.Threading.Tasks;

namespace Infrastructure.SceneLoader
{
	public interface ISceneLoader : IService
	{
		UniTask LoadAsync(string sceneName);
		UniTask LoadAsync(string sceneName, Action onLoaded);
	}
}