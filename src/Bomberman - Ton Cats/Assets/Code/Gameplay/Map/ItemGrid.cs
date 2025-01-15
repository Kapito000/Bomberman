namespace Gameplay.Map
{
	public sealed class ItemGrid : Grid<MapItem>
	{
		public ItemGrid(int xSize, int ySize) : base(xSize, ySize)
		{ }

		protected override bool IsEquals(MapItem a, MapItem b) =>
			a == b;
	}
}