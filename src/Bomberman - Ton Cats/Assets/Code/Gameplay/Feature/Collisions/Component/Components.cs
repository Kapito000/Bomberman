using System.Collections.Generic;
using Leopotam.EcsLite;

namespace Gameplay.Feature.Collisions.Component
{
	public struct TriggerExitBuffer { public List<EcsPackedEntityWithWorld> Value; }
	public struct TriggerEnterBuffer { public List<EcsPackedEntityWithWorld> Value; }
}