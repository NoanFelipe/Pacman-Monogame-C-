using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    public class Node
    {
        int gCost;
        int hCost;
        int fCost;

        bool isWalkable;

        Vector2 pos;

        public Node(Vector2 pos, Tile[,] tileArray)
        {

        }

        public Node(Vector2 pos)
        {
            this.pos = pos;
        }

        public Node createNode(Vector2 pos, Tile[,] tileArray)
        {
            Tile tile = tileArray[(int)pos.X, (int)pos.Y];
            Node node = new Node(pos);

            if (pos.X < 0 || pos.Y < 0 || pos.X >= Controller.numberOfTilesX || pos.Y >= Controller.numberOfTilesY)
                return new Node(new Vector2(-100, -100));

            if (tile.tileType == Tile.TileType.Wall)
            {
                node.isWalkable = false;
            }
            else
            {
                node.isWalkable = true;
            }

            return node;
        }
    }
}
