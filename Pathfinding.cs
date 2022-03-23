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
        public static List<Node> findPath(Vector2 startPos, Vector2 endPos, Tile[,] tileArray)
        {
            List<Node> openList = new List<Node>();
            List<Node> closedList = new List<Node>();

            Node startNode = new Node(startPos, tileArray);
            Node endNode = new Node(endPos, tileArray);
            openList.Add(startNode);

            bool foundPath = false;

            Node currentNode;
            while (openList != null)
            {
                currentNode = openList[0];
                for (int i = 1; i < openList.Count; i++)
                {
                    if (openList[i].fCost < currentNode.fCost || openList[i].fCost == currentNode.fCost && openList[i].hCost < currentNode.hCost)
                    {
                        currentNode = openList[i];
                    }
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                if (currentNode.pos == endNode.pos)
                {
                    foundPath = true;
                    break;
                }

                foreach (Node neighbour in currentNode.getNeighbours(tileArray))
                {
                    if (!neighbour.isWalkable || isNodeInsideList(neighbour, closedList))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + getDistance(currentNode, neighbour);
                    if (newMovementCostToNeighbour < neighbour.gCost || !isNodeInsideList(neighbour, openList))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = getDistance(neighbour, endNode);
                        neighbour.parent = currentNode;

                        if (!isNodeInsideList(neighbour, openList))
                        {
                            openList.Add(neighbour);
                        }
                    }
                }
            }

            if (foundPath)
            {
                return retracePath(startNode, endNode);
            }
            else
            {
                return new List<Node>();
            }
        }

        public static List<Node> retracePath (Node startNode, Node endNode)
        {
            List<Node> path = new List<Node> ();
            Node currentNode = endNode;

            while (currentNode.pos != startNode.pos)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }
            path.Reverse();

            return path;
        }

        public static bool isNodeInsideList(Node n, List<Node> nodeList)
        {
            foreach (Node node in nodeList)
            {
                if (node.pos == n.pos)
                {
                    return true;
                }
            }
            return false;
        }

        public static int getDistance(Node nodeA, Node nodeB)
        {
            int distX = (int)(nodeA.pos.X - nodeB.pos.X);
            int distY = (int)(nodeA.pos.Y - nodeB.pos.Y);

            if (distX < 0)
                distX *= -1;
            if (distY < 0)
                distY *= -1;

            int totalDist = distX + distY;

            return totalDist;
        }

    }
}
