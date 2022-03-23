﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    public class Controller
    {
        private int[,] mapDesign = new int[,]{
            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1},
            { 1,0,1,1,1,1,0,1,1,1,1,1,0,1,1,0,1,1,1,1,1,0,1,1,1,1,0,1},
            { 1,3,1,1,1,1,0,1,1,1,1,1,0,1,1,0,1,1,1,1,1,0,1,1,1,1,3,1},
            { 1,0,1,1,1,1,0,1,1,1,1,1,0,1,1,0,1,1,1,1,1,0,1,1,1,1,0,1},
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            { 1,0,1,1,1,1,0,1,1,0,1,1,1,1,1,1,1,1,0,1,1,0,1,1,1,1,0,1},
            { 1,0,1,1,1,1,0,1,1,0,1,1,1,1,1,1,1,1,0,1,1,0,1,1,1,1,0,1},
            { 1,0,0,0,0,0,0,1,1,0,0,0,0,1,1,0,0,0,0,1,1,0,0,0,0,0,0,1},
            { 1,1,1,1,1,1,0,1,1,1,1,1,5,1,1,5,1,1,1,1,1,0,1,1,1,1,1,1},
            { 1,1,1,1,1,1,0,1,1,1,1,1,5,1,1,5,1,1,1,1,1,0,1,1,1,1,1,1},
            { 1,1,1,1,1,1,0,1,1,5,5,5,5,5,5,5,5,5,5,1,1,0,1,1,1,1,1,1},
            { 1,1,1,1,1,1,0,1,1,5,1,1,1,2,2,1,1,1,5,1,1,0,1,1,1,1,1,1},
            { 1,1,1,1,1,1,0,1,1,5,1,2,2,2,2,2,2,1,5,1,1,0,1,1,1,1,1,1},
            { 0,0,0,0,0,0,0,5,5,5,1,2,2,2,2,2,2,1,5,5,5,0,0,0,0,0,0,0},
            { 1,1,1,1,1,1,0,1,1,5,1,2,2,2,2,2,2,1,5,1,1,0,1,1,1,1,1,1},
            { 1,1,1,1,1,1,0,1,1,5,1,1,1,1,1,1,1,1,5,1,1,0,1,1,1,1,1,1},
            { 1,1,1,1,1,1,0,1,1,5,5,5,5,5,5,5,5,5,5,1,1,0,1,1,1,1,1,1},
            { 1,1,1,1,1,1,0,1,1,5,1,1,1,1,1,1,1,1,5,1,1,0,1,1,1,1,1,1},
            { 1,1,1,1,1,1,0,1,1,5,1,1,1,1,1,1,1,1,5,1,1,0,1,1,1,1,1,1},
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1},
            { 1,0,1,1,1,1,0,1,1,1,1,1,0,1,1,0,1,1,1,1,1,0,1,1,1,1,0,1},
            { 1,0,1,1,1,1,0,1,1,1,1,1,0,1,1,0,1,1,1,1,1,0,1,1,1,1,0,1},
            { 1,3,0,0,1,1,0,0,0,0,0,0,0,5,5,0,0,0,0,0,0,0,1,1,0,0,3,1},
            { 1,1,1,0,1,1,0,1,1,0,1,1,1,1,1,1,1,1,0,1,1,0,1,1,0,1,1,1},
            { 1,1,1,0,1,1,0,1,1,0,1,1,1,1,1,1,1,1,0,1,1,0,1,1,0,1,1,1},
            { 1,0,0,0,0,0,0,1,1,0,0,0,0,1,1,0,0,0,0,1,1,0,0,0,0,0,0,1},
            { 1,0,1,1,1,1,1,1,1,1,1,1,0,1,1,0,1,1,1,1,1,1,1,1,1,1,0,1},
            { 1,0,1,1,1,1,1,1,1,1,1,1,0,1,1,0,1,1,1,1,1,1,1,1,1,1,0,1},
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
        };

        public static int numberOfTilesY;
        public static int numberOfTilesX;
        public static int tileWidth;
        public static int tileHeight;
        public Tile[,] tileArray;
        public List<Snack> snackList = new List<Snack>();

        public Controller()
        {
            numberOfTilesX = 28;
            numberOfTilesY = 31;
            tileWidth = Game1.windowWidth / numberOfTilesX;
            tileHeight = (Game1.windowHeight - Game1.scoreOffSet) / numberOfTilesY;
            tileArray = new Tile[numberOfTilesX, numberOfTilesY];
        }


        public void createGrid()
        {
            for (int y = 0; y < numberOfTilesY; y++)
            {
                for (int x = 0; x < numberOfTilesX; x++)
                {
                    if (mapDesign[y, x] == 0) // small snack
                    {
                        tileArray[x, y] = new Tile(new Vector2(x * tileWidth, y * tileHeight + Game1.scoreOffSet), Tile.TileType.Snack);
                        tileArray[x, y].isEmpty = false;
                        snackList.Add(new Snack(Snack.SnackType.Small, new Vector2(x * tileWidth, y * tileHeight + Game1.scoreOffSet), new int[] { x, y}));
                    }
                    else if (mapDesign[y, x] == 1) // wall collider
                    {
                        tileArray[x, y] = new Tile(new Vector2(x * tileWidth, y * tileHeight + Game1.scoreOffSet), Tile.TileType.Wall);
                        tileArray[x, y].isEmpty = false;
                    }
                    else if (mapDesign[y, x] == 2) //  ghost house
                    {
                        tileArray[x, y] = new Tile(new Vector2(x * tileWidth, y * tileHeight + Game1.scoreOffSet), Tile.TileType.GhostHouse);
                        tileArray[x, y].isEmpty = false;
                    }
                    else if (mapDesign[y, x] == 3) // big snack
                    {
                        tileArray[x, y] = new Tile(new Vector2(x * tileWidth, y * tileHeight + Game1.scoreOffSet), Tile.TileType.Snack);
                        tileArray[x, y].isEmpty = false;
                        snackList.Add(new Snack(Snack.SnackType.Big, new Vector2(x * tileWidth, y * tileHeight + Game1.scoreOffSet), new int[] { x, y }));
                    }
                    else if (mapDesign[y, x] == 5) // empty
                    {
                        tileArray[x, y] = new Tile(new Vector2(x * tileWidth, y * tileHeight + Game1.scoreOffSet));
                    }
                }
            }
        }

        public void drawGridDebugger(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < numberOfTilesX; x++)
            {
                for (int y = 0; y < numberOfTilesY; y++)
                {
                    Vector2 dotPosition = tileArray[x, y].Position;
                    spriteBatch.Draw(Game1.debugLineX, dotPosition, Color.White);
                    spriteBatch.Draw(Game1.debugLineY, dotPosition, Color.White);
                    
                }
            }
        }
        public void drawPacmanGridDebugger(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < numberOfTilesX; x++)
            {
                for (int y = 0; y < numberOfTilesY; y++)
                {
                    Vector2 dotPosition = tileArray[x, y].Position;
                    if (tileArray[x, y].tileType == Tile.TileType.Player)
                    {
                        spriteBatch.Draw(Game1.playerDebugLineX, dotPosition, Color.White);
                        spriteBatch.Draw(Game1.playerDebugLineY, dotPosition, Color.White);
                        spriteBatch.Draw(Game1.playerDebugLineX, new Vector2(dotPosition.X, dotPosition.Y + 24), Color.White);
                        spriteBatch.Draw(Game1.playerDebugLineY, new Vector2(dotPosition.X + 24, dotPosition.Y), Color.White);
                    }
                }
            }
        }

        public void drawPathFindingDebugger(SpriteBatch spriteBatch, List<Node> nodes)
        {
            foreach (Node node in nodes)
            {
                Vector2 nodePos = tileArray[(int)node.pos.X, (int)node.pos.Y].Position;
                spriteBatch.Draw(Game1.playerDebugLineX, nodePos, Color.White);
                spriteBatch.Draw(Game1.playerDebugLineY, nodePos, Color.White);
                spriteBatch.Draw(Game1.playerDebugLineX, new Vector2(nodePos.X, nodePos.Y + 24), Color.White);
                spriteBatch.Draw(Game1.playerDebugLineY, new Vector2(nodePos.X + 24, nodePos.Y), Color.White);
            }
        }

        public bool isNextTileAvailable(Dir dir, int[] tile)
        { // tile != new int[2] {0, 14} && tile != new int[2] {numberOfTilesX-1 ,14}
            if (tile.SequenceEqual(new int[2] { 0, 14 }) || tile.SequenceEqual(new int[2] { numberOfTilesX - 1 ,14}))
            {
                if (dir == Dir.Right || dir == Dir.Left)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                switch (dir)
                {
                    case Dir.Right:
                        if (tileArray[tile[0] + 1, tile[1]].tileType == Tile.TileType.Wall || tileArray[tile[0] + 1, tile[1]].tileType == Tile.TileType.GhostHouse)
                        {
                            return false;
                        }
                        break;
                    case Dir.Left:
                        if (tileArray[tile[0] - 1, tile[1]].tileType == Tile.TileType.Wall || tileArray[tile[0] - 1, tile[1]].tileType == Tile.TileType.GhostHouse)
                        {
                            return false;
                        }
                        break;
                    case Dir.Down:
                        if (tileArray[tile[0], tile[1] + 1].tileType == Tile.TileType.Wall || tileArray[tile[0], tile[1] + 1].tileType == Tile.TileType.GhostHouse)
                        {
                            return false;
                        }
                        break;
                    case Dir.Up:
                        if (tileArray[tile[0], tile[1] - 1].tileType == Tile.TileType.Wall || tileArray[tile[0], tile[1] - 1].tileType == Tile.TileType.GhostHouse)
                        {
                            return false;
                        }
                        break;
                }
                return true;
            }
        }

        public int findSnackListPosition(Vector2 snackGridPos)
        {
            int listPosition = -1;
            foreach (Snack snack in snackList)
            {
                if (snack.Position == snackGridPos)
                {
                    listPosition = snackList.IndexOf(snack);
                }
            }

            return listPosition;
        }

        public bool checkTileType(int[] gridIndex, Tile.TileType tileType)
        {
            bool tile = false;
            if (tileArray[gridIndex[0],gridIndex[1]].tileType == tileType)
            {
                tile = true;
            }
            return tile;
        }
    }
}
