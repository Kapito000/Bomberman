using UnityEngine;
using Zenject;

namespace Infrastructure.Boot
{
	public sealed class GameBootstrapper : MonoBehaviour
	{
		[Inject] Game _game;

		void Start()
		{
			_game.Start();
		}
	}
}