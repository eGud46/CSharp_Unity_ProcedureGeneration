using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TileGroupType
{
    Water,
    Land
}


public class TileGroup
{
    public TileGroupType Type;
    public List<Tile> Tiles;

    public TileGroup()
    {
        Tiles = new List<Tile>();
    }
}
