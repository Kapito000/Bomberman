using Infrastructure.GameStatus;
using Infrastructure.GameStatus.State;
using StaticData.SceneNames;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Infrastructure.Boot
{
	public sealed class DevSceneRunner : MonoBehaviour, IDevSceneRunner
	{
		[Inject] Game _game;
		[Inject] IGameStateMachine _gameStateMachine;
		[Inject] ISceneNameData _sceneNameData;

		public bool TryRunScene()
		{
			if (_game.Started)
			{
				Destroy(gameObject);
				return false;
			}

			_game.CustomScene = SceneManager.GetActiveScene().name;
			var boot = _sceneNameData.Boot;
			_gameStateMachine.GetState<LoadScene>().LoadingSceneName = boot;
			_gameStateMachine.Enter<LoadScene>();
			return true;
		}
	}
}