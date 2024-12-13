using UnityEngine.AI;

namespace Extensions
{
	public static class NavMeshExtension
	{
		public static int GetAgentTypeIDByName(string agentTypeName)
		{
			int count = NavMesh.GetSettingsCount();
			for (var i = 0; i < count; i++)
			{
				int id = NavMesh.GetSettingsByIndex(i).agentTypeID;
				string name = NavMesh.GetSettingsNameFromID(id);
				if (name == agentTypeName)
					return id;
			}
			return -1;
		}

		public static bool TryGetAgentTypeIDByName(string agentTypeName, out int id)
		{
			id = GetAgentTypeIDByName(agentTypeName);
			return id != -1;
		}
	}
}