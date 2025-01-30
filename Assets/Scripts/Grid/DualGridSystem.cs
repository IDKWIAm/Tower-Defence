using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static TowerDefence.Grid.TileType;

namespace TowerDefence.Grid
{
    public class DualGridSystem : MonoBehaviour
    {
        protected static Vector3Int[] NEIGHBOURS = new Vector3Int[] {
        new Vector3Int(0, 0, 0),
        new Vector3Int(1, 0, 0),
        new Vector3Int(0, 1, 0),
        new Vector3Int(1, 1, 0)
    };

        protected static Dictionary<Tuple<TileType, TileType, TileType, TileType>, Tile> neighbourTupleToTile;

        public Tilemap placeholderTilemap;
        public Tilemap displayTilemap;

        public Tile grassPlaceholderTile;
        public Tile sandPlaceholderTile;

        public Tile[] tiles;

        void Start()
        {
            neighbourTupleToTile = new() {
            {new (Grass, Grass, Grass, Grass), tiles[6]},
            {new (Sand, Sand, Sand, Grass), tiles[13]}, // OUTER_BOTTOM_RIGHT
            {new (Sand, Sand, Grass, Sand), tiles[0]}, // OUTER_BOTTOM_LEFT
            {new (Sand, Grass, Sand, Sand), tiles[8]}, // OUTER_TOP_RIGHT
            {new (Grass, Sand, Sand, Sand), tiles[15]}, // OUTER_TOP_LEFT
            {new (Sand, Grass, Sand, Grass), tiles[1]}, // EDGE_RIGHT
            {new (Grass, Sand, Grass, Sand), tiles[11]}, // EDGE_LEFT
            {new (Sand, Sand, Grass, Grass), tiles[3]}, // EDGE_BOTTOM
            {new (Grass, Grass, Sand, Sand), tiles[9]}, // EDGE_TOP
            {new (Sand, Grass, Grass, Grass), tiles[5]}, // INNER_BOTTOM_RIGHT
            {new (Grass, Sand, Grass, Grass), tiles[2]}, // INNER_BOTTOM_LEFT
            {new (Grass, Grass, Sand, Grass), tiles[10]}, // INNER_TOP_RIGHT
            {new (Grass, Grass, Grass, Sand), tiles[7]}, // INNER_TOP_LEFT
            {new (Sand, Grass, Grass, Sand), tiles[14]}, // DUAL_UP_RIGHT
            {new (Grass, Sand, Sand, Grass), tiles[4]}, // DUAL_DOWN_RIGHT
            {new (Sand, Sand, Sand, Sand), tiles[12]},
        };
            RefreshDisplayTilemap();
        }

        public void SetCell(Vector3Int coords, Tile tile)
        {
            placeholderTilemap.SetTile(coords, tile);
            setDisplayTile(coords);
        }

        private TileType getPlaceholderTileTypeAt(Vector3Int coords)
        {
            if (placeholderTilemap.GetTile(coords) == grassPlaceholderTile)
                return Grass;
            else
                return Sand;
        }

        protected Tile calculateDisplayTile(Vector3Int coords)
        {
            // 4 neighbours
            TileType topRight = getPlaceholderTileTypeAt(coords - NEIGHBOURS[0]);
            TileType topLeft = getPlaceholderTileTypeAt(coords - NEIGHBOURS[1]);
            TileType botRight = getPlaceholderTileTypeAt(coords - NEIGHBOURS[2]);
            TileType botLeft = getPlaceholderTileTypeAt(coords - NEIGHBOURS[3]);

            Tuple<TileType, TileType, TileType, TileType> neighbourTuple = new(topLeft, topRight, botLeft, botRight);

            return neighbourTupleToTile[neighbourTuple];
        }

        protected void setDisplayTile(Vector3Int pos)
        {
            for (int i = 0; i < NEIGHBOURS.Length; i++)
            {
                Vector3Int newPos = pos + NEIGHBOURS[i];
                displayTilemap.SetTile(newPos, calculateDisplayTile(newPos));
            }
        }

        public void RefreshDisplayTilemap()
        {
            for (int i = -50; i < 50; i++)
            {
                for (int j = -50; j < 50; j++)
                {
                    setDisplayTile(new Vector3Int(i, j, 0));
                }
            }
        }
    }

    public enum TileType
    {
        None,
        Grass,
        Sand
    }
}
