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
        public static List<Vector2> findPath(Vector2 startPos, Vector2 endPos, Tile[,] tileArray, Dir currentDirection)
        {
            List<Node> openList = new List<Node>();
            List<Node> closedList = new List<Node>();

            Node startNode = new Node(startPos, tileArray);
            Node endNode = new Node(endPos, tileArray);
            startNode.setIgnoreDirection(currentDirection);
            openList.Add(startNode.Copy(tileArray));

            bool foundPath = false;
            Node currentNode = openList[0].Copy(tileArray);
            while (openList.Count > 0)
            {
                currentNode = openList[0].Copy(tileArray);
                for (int i = 1; i < openList.Count; i++)
                {
                    if (openList[i].fCost < currentNode.fCost || openList[i].fCost == currentNode.fCost && openList[i].hCost < currentNode.hCost)
                    {
                        currentNode = openList[i].Copy(tileArray);
                    }
                }

                deleteNodeOnList(currentNode, openList);
                closedList.Add(currentNode.Copy(tileArray));

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
                        neighbour.setParent(currentNode.Copy(tileArray));

                        if (!isNodeInsideList(neighbour, openList))
                        {
                            openList.Add(neighbour.Copy(tileArray));
                        }
                    }
                }
            }

            if (foundPath)
            {
                List<Vector2> path = new List<Vector2>();
                while(currentNode.pos != startNode.pos)
                {
                    path.Add(currentNode.pos);
                    currentNode = currentNode.parent.Copy(tileArray);
                }
                path.Reverse();
                return path;
            }
            else
            {
                return new List<Vector2>();
            }
        }

        public static void deleteNodeOnList(Node n, List<Node> nodeList)
        {
            for (int i = 0; i < nodeList.Count; i++)
            {
                if (nodeList[i].pos == n.pos)
                {
                    nodeList.RemoveAt(i);
                    break;
                }
            }
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
