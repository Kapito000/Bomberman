using Map;
using MapView;

namespace MapController
{
	public interface IMapController : IMap
	{
		IMapView View { get; }
		void SetMap(IMap map);
	}
}