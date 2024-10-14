﻿using UnityEngine;
using UnityEngine.Tilemaps;
using Menu = Constant.CreateAssetMenu;

namespace MapTile
{
	[CreateAssetMenu(menuName = Menu.Path.c_MapTile + nameof(BaseGameTile))]
	public class BaseGameTile : Tile
	{ }
}