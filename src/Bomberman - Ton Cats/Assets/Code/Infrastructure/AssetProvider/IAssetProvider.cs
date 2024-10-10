using UnityEngine;

namespace Infrastructure.AssetProvider
{
	public interface IAssetProvider : IService
	{
		Camera Camera();
		GameObject Hero();
	}
}