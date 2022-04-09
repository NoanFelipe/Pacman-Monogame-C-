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
        public Dir ignoreDirection = Dir.None;

        public void setIgnoreDirection(Dir currentDir)
        {
            switch (currentDir)
            {
                case Dir.Right:
                    ignoreDirection = Dir.Left;
                    break;
                case Dir.Left:
                    ignoreDirection = Dir.Right;
                    break;
                case Dir.Down:
                    ignoreDirection = Dir.Up;
                    break;
                case Dir.Up:
                    ignoreDirection = Dir.Down;
                    break;
            }
        }

        public void setParent(Node parent)
        {
            this.parent = parent;
        }
        
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

        public Node Copy(Tile[,] tileArray)
        {
            Node node = new Node(pos, tileArray);

            node.hCost = hCost;
            node.gCost = gCost;
            node.ignoreDirection = ignoreDirection;
            if (parent != null)
            {
                node.setParent(parent.Copy(tileArray));
            }

            return node;
        }

        public List<Node> getNeighbours(Tile[,] tileArray)
        {
            List<Node> neighbours = new List<Node>();

            if (ignoreDirection != Dir.Left)
            { 
                Node left = new Node(new Vector2(pos.X - 1, pos.Y), tileArray);
                if (left.pos != new Vector2(-100, -100)) neighbours.Add(left);
            }

            if (ignoreDirection != Dir.Right)
            {
                Node right = new Node(new Vector2(pos.X + 1, pos.Y), tileArray);
                if (right.pos != new Vector2(-100, -100)) neighbours.Add(right);
            }

            if (ignoreDirection != Dir.Up)
            {
                Node up = new Node(new Vector2(pos.X, pos.Y - 1), tileArray);
                if (up.pos != new Vector2(-100, -100)) neighbours.Add(up);
            }

            if (ignoreDirection != Dir.Down)
            {
                Node down = new Node(new Vector2(pos.X, pos.Y + 1), tileArray);
                if (down.pos != new Vector2(-100, -100)) neighbours.Add(down);
            }

            return neighbours;
        }
    }
}
