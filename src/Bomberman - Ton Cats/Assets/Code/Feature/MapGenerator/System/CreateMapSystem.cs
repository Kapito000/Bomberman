using Feature.MapGenerator.Services;
using Leopotam.EcsLite;
using LevelData;
using UnityEngine;
using Zenject;

namespace Feature.MapGenerator.System
{
	public sealed class CreateMapSystem : IEcsRunSystem
	{
		[Inject] ILevelData _levelData;
		[Inject] IMapGenerator _mapGenerator;

		public async void Run(IEcsSystems systems)
		{
			_levelData.Map = await _mapGenerator.CreateMapAsync(new ProgressDebugger());
		}
	}

	// Remove me.
	public sealed class ProgressDebugger : IGenerateMapProgress
	{
		public void Report(float value)
		{
			Debug.Log(value);
		}
	}
}