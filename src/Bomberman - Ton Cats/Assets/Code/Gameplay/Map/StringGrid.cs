namespace Gameplay.Map
{
	public sealed class StringGrid : Grid<string>
	{
		public StringGrid(int xSize, int ySize) : base(xSize, ySize)
		{ }

		protected override bool IsEquals(string a, string b) =>
			a == b;
	}
}