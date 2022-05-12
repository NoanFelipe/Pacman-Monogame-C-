using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    public class Tile
    {
        public enum TileType { None, Wall, Ghost, GhostHouse, Player, Snack };

        public TileType tileType = TileType.None;
        public bool isEmpty = true;

        Vector2 position;

        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        public static int getDistanceBetweenTiles(Vector2 pos1, Vector2 pos2)
        {
            return (int)Math.Sqrt(Math.Pow(pos1.X - pos2.X, 2) + Math.Pow(pos1.Y - pos2.Y, 2));
        }

        public Tile(Vector2 newPosition)
        {
            position = newPosition;
        }

        public Tile(Vector2 newPosition, TileType newTileType)
        {
            position = newPosition;
            tileType = newTileType;
        }
    }
}
