using NMesh = UnityEngine.AI.NavMesh;

namespace Constant.NavMesh
{
	public static class AreaMask
	{
		public static readonly int Walkable;
		public static readonly int NotWalkable;

		static AreaMask()
		{
			Walkable = NMesh.GetAreaFromName("Walkable");
			NotWalkable = NMesh.GetAreaFromName("Not Walkable");
		}

		public static class Name
		{
			public const string c_Walkable = "Walkable";
			public const string c_NotWalkable = "Not Walkable";
		}
	}
}