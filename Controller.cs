using System;
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

        public void createGrid() // creates grid that contains Tile objects, which represent 24x24 pixels squares in the game, all with types such as walls, snacks and etc.
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

        public static void drawGhosts(Inky i, Blinky b, Pinky p, Clyde c, SpriteBatch spriteBatch, SpriteSheet spriteSheet)
        {
            i.Draw(spriteBatch, spriteSheet);
            b.Draw(spriteBatch, spriteSheet);
            p.Draw(spriteBatch, spriteSheet);
            c.Draw(spriteBatch, spriteSheet);
        }

        public static void updateGhosts(Inky i, Blinky b, Pinky p, Clyde c, GameTime gameTime, Tile[,] tileArray, Vector2 playerTilePos)
        {
            i.Update(gameTime, tileArray, playerTilePos);
            b.Update(gameTime, tileArray, playerTilePos);
            p.Update(gameTime, tileArray, playerTilePos);
            c.Update(gameTime, tileArray, playerTilePos);
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

        public void gameOver()
        {

        }

        public void drawPathFindingDebugger(SpriteBatch spriteBatch, List<Vector2> path)
        {
            if (path == null) return;
            foreach (Vector2 gridPos in path)
            {
                Vector2 pos = tileArray[(int)gridPos.X, (int)gridPos.Y].Position;
                spriteBatch.Draw(Game1.pathfindingDebugLineX, pos, Color.White);
                spriteBatch.Draw(Game1.pathfindingDebugLineY, pos, Color.White);
                spriteBatch.Draw(Game1.pathfindingDebugLineX, new Vector2(pos.X, pos.Y + 24), Color.White);
                spriteBatch.Draw(Game1.pathfindingDebugLineY, new Vector2(pos.X + 24, pos.Y), Color.White);
            }
        }

        public bool isNextTileAvailable(Dir dir, Vector2 tile)
        { // tile != new int[2] {0, 14} && tile != new int[2] {numberOfTilesX-1 ,14}
            if (tile.Equals(new Vector2( 0, 14 )) || tile.Equals(new Vector2(numberOfTilesX - 1 ,14)))
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
                        if (tileArray[(int)tile.X + 1, (int)tile.Y].tileType == Tile.TileType.Wall || tileArray[(int)tile.X + 1, (int)tile.Y].tileType == Tile.TileType.GhostHouse)
                        {
                            return false;
                        }
                        break;
                    case Dir.Left:
                        if (tileArray[(int)tile.X - 1, (int)tile.Y].tileType == Tile.TileType.Wall || tileArray[(int)tile.X - 1, (int)tile.Y].tileType == Tile.TileType.GhostHouse)
                        {
                            return false;
                        }
                        break;
                    case Dir.Down:
                        if (tileArray[(int)tile.X, (int)tile.Y + 1].tileType == Tile.TileType.Wall || tileArray[(int)tile.X, (int)tile.Y + 1].tileType == Tile.TileType.GhostHouse)
                        {
                            return false;
                        }
                        break;
                    case Dir.Up:
                        if (tileArray[(int)tile.X, (int)tile.Y - 1].tileType == Tile.TileType.Wall || tileArray[(int)tile.X, (int)tile.Y - 1].tileType == Tile.TileType.GhostHouse)
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

        public bool checkTileType(Vector2 gridIndex, Tile.TileType tileType)
        {
            bool tile = false;
            if (tileArray[(int)gridIndex.X, (int)gridIndex.Y].tileType == tileType)
            {
                tile = true;
            }
            return tile;
        }
    }
}
