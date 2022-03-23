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
        public int gCost;
        public int hCost;

        public Node parent;
        
        public int fCost {
            get {
                return gCost + hCost;
            }
        }

        public bool isWalkable;

        public Vector2 pos;
        Tile tile;

        public Node(Vector2 pos, Tile[,] tileArray)
        {
            if (pos.X < 0 || pos.Y < 0 || pos.X >= Controller.numberOfTilesX || pos.Y >= Controller.numberOfTilesY)
            {
                this.pos = new Vector2(-100, -100);
            }
            else
            {
                this.pos = pos;
                tile = tileArray[(int)pos.X, (int)pos.Y];
                if (tile.tileType == Tile.TileType.Wall)
                {
                    isWalkable = false;
                }
                else
                {
                    isWalkable = true;
                }
            }

        }

        public List<Node> getNeighbours(Tile[,] tileArray)
        {
            List<Node> neighbours = new List<Node>();

            Node left = new Node(new Vector2(pos.X - 1, pos.Y), tileArray);
            if (left.pos != new Vector2(-100, -100)) neighbours.Add(left);

            Node right = new Node(new Vector2(pos.X + 1, pos.Y), tileArray);
            if (right.pos != new Vector2(-100, -100)) neighbours.Add(right);

            Node up = new Node(new Vector2(pos.X, pos.Y -1), tileArray);
            if (up.pos != new Vector2(-100, -100)) neighbours.Add(up);

            Node down = new Node(new Vector2(pos.X, pos.Y + 1), tileArray);
            if (down.pos != new Vector2(-100, -100)) neighbours.Add(down);

            return neighbours;
        }
    }
}
