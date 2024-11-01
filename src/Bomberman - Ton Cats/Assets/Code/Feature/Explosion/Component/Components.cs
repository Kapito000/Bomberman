using MapTile;
using UnityEngine;

namespace Feature.Explosion.Component
{
	public struct Explosion { }
	public struct ExplosionRequest { }
	public struct CreateExplosionRequest { }
	public struct BlowUpDestructible { public IDestructible Destructible; }
	public struct DestructibleTileCellPos { public Vector3Int Value; }
	public struct ExplosionPart { public EExplosionPart Value; }
	public struct ExplosionCenter { }
}