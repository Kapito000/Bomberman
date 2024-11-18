using UnityEngine;

namespace Gameplay.Map
{
	public interface ICellCreator
	{
		void Create(CellType type, Vector2 position);
	}
}