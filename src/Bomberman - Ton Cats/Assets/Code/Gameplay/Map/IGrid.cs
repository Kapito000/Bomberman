using System.Collections.Generic;

namespace Gameplay.Map
{
	public interface IGrid
	{
		ref Cell this[int x, int y] { get; }
		bool Has(int x, int y);
	}
}