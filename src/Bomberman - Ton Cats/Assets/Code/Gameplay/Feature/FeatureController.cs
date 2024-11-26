using System;
using System.Collections.Generic;
using Gameplay.EndGame;
using Gameplay.Feature.Bomb;
using Gameplay.Feature.Camera;
using Gameplay.Feature.Collisions;
using Gameplay.Feature.DamageApplication;
using Gameplay.Feature.Destruction;
using Gameplay.Feature.Enemy;
using Gameplay.Feature.Explosion;
using Gameplay.Feature.Hero;
using Gameplay.Feature.HUD;
using Gameplay.Feature.Input;
using Gameplay.Feature.Life;
using Gameplay.Feature.Map;
using Gameplay.Feature.MapGenerator;
using Gameplay.Feature.UI;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature
{
	public sealed class FeatureController : IDisposable
	{
		readonly ISystemFactory _systemFactory;
		readonly List<Infrastructure.ECS.Feature> _features = new();

		public FeatureController(ISystemFactory systemFactory)
		{
			_systemFactory = systemFactory;
			Add<InputFeature>();
			Add<MapGenerationFeature>();
			Add<CollisionsFeature>();
			Add<HeroFeature>();
			Add<EnemyFeature>();
			Add<BombFeature>();
			Add<ExplosionFeature>();
			Add<DamageApplicationFeature>();
			Add<LifeFeature>();
			Add<MapFeature>();
			Add<EndGameFeature>();
			Add<CameraFeature>();
			Add<UiFeature>();
			Add<HudFeature>();
			Add<DestructionFeature>();
		}

		public void Init() =>
			_features.ForEach(f => f.Init());

		public void Start() =>
			_features.ForEach(f => f.Start());

		public void Update() =>
			_features.ForEach(f => f.Update());

		public void FixedUpdate() =>
			_features.ForEach(f => f.FixedUpdate());

		public void LateUpdate() =>
			_features.ForEach(f => f.LateUpdate());
		
		public void Cleanup() =>
			_features.ForEach(f => f.Cleanup());

		public void Dispose()
		{
			_features.ForEach(f => f.Dispose());
			_features.Clear();
		}

		void Add(Infrastructure.ECS.Feature feature) =>
			_features.Add(feature);

		void Add<TFeature>() where TFeature : Infrastructure.ECS.Feature =>
			_features.Add(_systemFactory.Create<TFeature>());
	}
}