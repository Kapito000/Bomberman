namespace Gameplay.Map
{
	public sealed class SpawnCells : Grid<SpawnCellType>
	{
		public SpawnCells(int xSize, int ySize) : base(xSize, ySize)
		{ }

		protected override bool IsEquals(SpawnCellType a, SpawnCellType b) =>
			a == b;
	}
}