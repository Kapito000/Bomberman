using System.Collections.Generic;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature.FeatureControl
{
	public abstract class FeatureController : IFeatureController
	{
		readonly ISystemFactory _systemFactory;
		readonly List<Infrastructure.ECS.Feature> _features = new();

		protected FeatureController(ISystemFactory systemFactory)
		{
			_systemFactory = systemFactory;
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

		protected void Add(Infrastructure.ECS.Feature feature) =>
			_features.Add(feature);

		protected void Add<TFeature>() where TFeature : Infrastructure.ECS.Feature =>
			_features.Add(_systemFactory.Create<TFeature>());
	}
}