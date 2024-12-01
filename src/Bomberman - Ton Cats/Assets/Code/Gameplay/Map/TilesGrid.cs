namespace Gameplay.Map
{
	public sealed class TilesGrid : Grid<TileType>
	{
		public TilesGrid(int xSize, int ySize) : base(xSize, ySize)
		{ }

		protected override bool IsEquals(TileType a, TileType b) =>
			a == b;
	}
}