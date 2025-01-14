using UnityEngine;
using Zenject;
using Menu = Constant.CreateAssetMenu;

namespace Infrastructure.Installer
{
	[CreateAssetMenu(menuName =
		Menu.Path.c_Installers + nameof(TsvDataInstaller))]
	public sealed class TsvDataInstaller : ScriptableObjectInstaller
	{
		[Header("Bomb")]
		[SerializeField] TextAsset _bombData;
		[Header("Enemies at levels")]
		[SerializeField] TextAsset _enemiesAtDoorTable;
		[SerializeField] TextAsset _enemiesAtStartTable;
		[Header("Bonuses")]
		[SerializeField] TextAsset _bonusesTable;
		[SerializeField] TextAsset _additionalBombsBonuses;

		public override void InstallBindings()
		{
			BindBomb();
			BindBonuses();
			BindEnemiesData();
		}

		void BindEnemiesData()
		{
			Bind(Constant.TsvDataId.c_EnemiesAtDoor, _enemiesAtDoorTable);
			Bind(Constant.TsvDataId.c_EnemiesAtStart, _enemiesAtStartTable);
		}

		void BindBonuses()
		{
			Bind(Constant.TsvDataId.c_Bonuses, _bonusesTable);
			Bind(Constant.TsvDataId.c_AdditionalBombBonuses, _additionalBombsBonuses);
		}

		void BindBomb() =>
			Bind(Constant.TsvDataId.c_Bomb, _bombData);

		void Bind(string id, TextAsset tsv) =>
			Container.BindInstance(tsv).WithId(id);
	}
}