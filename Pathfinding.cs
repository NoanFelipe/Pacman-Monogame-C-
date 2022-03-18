using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    public class Pathfinding
    {
        public List<Node> openList;
        public List<Node> closedList;

        public void findPath(Vector2 startPos, Vector2 endPos, Dir currentDir, Tile[,] tileArray)
        {
            Node startNode = new Node().createNode(startPos, tileArray);
            Node endNode = new Node().createNode(endPos,tileArray);
            openList.Add(startNode);

            Node currentNode = openList[0];

            while (openList != null)
            {
                
            }
        }

    }
}
