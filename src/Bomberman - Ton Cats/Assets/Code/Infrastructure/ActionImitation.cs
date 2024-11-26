using UnityEngine;

namespace Infrastructure
{
	public static class ActionImitation
	{
		const string c_prefixColor = "blue";
		const string c_prefix = "<color=" + c_prefixColor + ">Imitation: </color>";

		public static void Execute(string msg)
		{
			Debug.Log(c_prefix + msg);
		}
	}
}