namespace Gameplay.Map
{
	public sealed class SpawnGrid : Grid<SpawnCellType>
	{
		public SpawnGrid(int xSize, int ySize) : base(xSize, ySize)
		{ }

		protected override bool IsEquals(SpawnCellType a, SpawnCellType b) =>
			a == b;
	}
}